using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Services.GameFetcher
{
    [Serializable()]
    public class GameFetcherException : Exception
    {

        public GameFetcherException(string errorMessage)
            : base(errorMessage)
        {
        }
        public GameFetcherException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
        }


    }
}