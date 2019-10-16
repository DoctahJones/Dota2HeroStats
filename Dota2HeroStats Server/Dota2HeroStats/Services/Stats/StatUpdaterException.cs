using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Services.Stats
{
    [Serializable()]
    public class StatUpdaterException : Exception
    {
        public StatUpdaterException(string errorMessage)
            : base(errorMessage)
        {
        }

        public StatUpdaterException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
        {
        }
    }
}