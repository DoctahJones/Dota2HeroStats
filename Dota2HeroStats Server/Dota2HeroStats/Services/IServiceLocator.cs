using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Services
{
    public interface IServiceLocator
    {
        T GetService<T>();

        void AddOrEditService<T>(T service);
    }
}