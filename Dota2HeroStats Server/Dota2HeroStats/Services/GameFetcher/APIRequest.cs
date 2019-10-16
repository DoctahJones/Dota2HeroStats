using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dota2HeroStats.Services.GameFetcher
{
    public abstract class APIRequest
    {
        public IAPIRequestSender apiRequestSender { get; set; }

        public string Url { get; protected set; }

        public APIRequest(IAPIRequestSender requestSender)
        {
            apiRequestSender = requestSender;
        }

        public string SendRequest()
        {
            return apiRequestSender.MakeRequest(Url);
        }

        public async Task<string> SendRequestAsync(CancellationToken cancelToken, TimeSpan timeOut)
        {
            return await apiRequestSender.MakeRequestAsync(Url, cancelToken, timeOut);
        }

    }
}
