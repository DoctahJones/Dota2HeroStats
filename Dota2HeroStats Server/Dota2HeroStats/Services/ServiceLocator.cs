using Dota2HeroStats.Services.GameFetcher;
using Dota2HeroStats.Services.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Services
{
    public class ServiceLocator : IServiceLocator
    {
        private IDictionary<object, object> services;

        private static IServiceLocator instance;

        private static readonly object objectLock = new Object();

        private ServiceLocator()
        {
            services = new Dictionary<object, object>();

            
            this.services.Add(typeof(IAPIRequestSender), new APIRequestSenderJSON());
            this.services.Add(typeof(IRateLimitedAPIRequestDispatcher), new RateLimitedAPIRequestDispatcher());
            this.services.Add(typeof(IDataSource), new DatabaseDataSource());
            this.services.Add(typeof(IGameFetcher), new OpenDotaGameFetcher(GetService<IDataSource>(), GetService<IAPIRequestSender>(), GetService<IRateLimitedAPIRequestDispatcher>()));

            var statUpdaters = new List<ISpecificStatUpdater>();
            statUpdaters.Add(new HeroAbilityStatUpdater(GetService<IDataSource>()));
            this.services.Add(typeof(IStatUpdater), new StatUpdater(statUpdaters));
        }

        public static IServiceLocator GetInstance()
        {
            lock (objectLock)
            {
                if (instance == null)
                {
                    instance = new ServiceLocator();
                }
                return instance;
            }
        }

        public T GetService<T>()
        {
            try
            {
                return (T)services[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("The requested service is not registered");
            }
        }
        public void AddOrEditService<T>(T service)
        {
            object existingService;
            services.TryGetValue(typeof(T), out existingService);
            if (existingService != null)
            {
                services[typeof(T)] = service;
            }
            else
            {
                this.services.Add(typeof(T), service);
            }

        }
    }
}