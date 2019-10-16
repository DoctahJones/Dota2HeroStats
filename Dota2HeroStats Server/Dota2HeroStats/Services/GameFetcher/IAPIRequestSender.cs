using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dota2HeroStats.Services.GameFetcher
{
    public interface IAPIRequestSender
    {
        string MakeRequest(string url);
        Task<string> MakeRequestAsync(string url, CancellationToken cancelToken, TimeSpan timeOut);

    }
}
