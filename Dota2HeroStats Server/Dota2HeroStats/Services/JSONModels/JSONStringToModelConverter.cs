using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Services.JSONModels
{
    public class JSONStringToModelConverter
    {
        public JSONStringToModelConverter()
        {
        }

        public T ConvertToModel<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

    }
}