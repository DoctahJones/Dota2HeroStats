using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dota2HeroStats.Services.GameFetcher
{
    /// <summary>
    /// Class that sends web requests with a delay.
    /// </summary>
    public class RateLimitedAPIRequestDispatcher : IRateLimitedAPIRequestDispatcher, IDisposable
    {
        /// <summary>
        /// Semaphore that prevents more than 1 task running at a time.
        /// </summary>
        private SemaphoreSlim semaphore = new SemaphoreSlim(1);

        /// <summary>
        /// The delay between APIRequests.
        /// </summary>
        public TimeSpan DelayPeriod { get; set; }


        public RateLimitedAPIRequestDispatcher()
            : this(TimeSpan.FromSeconds(2))
        {
        }

        public RateLimitedAPIRequestDispatcher(TimeSpan delayPeriod)
        {
            DelayPeriod = delayPeriod;
        }


        public async Task<string> MakeAPIRequestAsync(APIRequest request, CancellationToken cancelToken, TimeSpan timeOut)
        {
            await semaphore.WaitAsync(cancelToken);

            try
            {
                var timer = Task.Delay(DelayPeriod);
                var task = request.SendRequestAsync(cancelToken, timeOut);
                var taskAndTimer = new Task[] { task, timer };
                await Task.WhenAll(taskAndTimer);
                return task.Result;
            }
            finally
            {
                semaphore.Release();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (semaphore != null) semaphore.Dispose();
            }
        }
    }
}
