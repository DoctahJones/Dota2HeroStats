using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Services
{
    [Serializable()]
    public class DataSourceException : Exception
    {
        public DataSourceException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
        }

    }
}