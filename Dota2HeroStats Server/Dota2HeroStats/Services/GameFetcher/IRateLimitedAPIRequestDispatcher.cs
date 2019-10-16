using System;
using System.Threading;
using System.Threading.Tasks;
namespace Dota2HeroStats.Services.GameFetcher
{
    public interface IRateLimitedAPIRequestDispatcher
    {
        Task<string> MakeAPIRequestAsync(APIRequest request, CancellationToken cancelToken, TimeSpan timeOut);
    }
}
