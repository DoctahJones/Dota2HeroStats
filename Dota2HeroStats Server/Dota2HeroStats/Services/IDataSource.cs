using Dota2HeroStats.Models;
using System;
using System.Threading.Tasks;
namespace Dota2HeroStats.Services
{
    public interface IDataSource
    {
        Task<Ability> LookupAbility(int abilityId);
        Task<Hero> LookupHero(int heroId);
        Task<Role> LookupRoleByName(string roleName);
        Task<HeroAbilityStat> LookupHeroAbilityStat(int heroId, int abilityId);
        Task AddEntity<T>(T entity) where T : class, IEntity;
        Task UpdateEntity<T>(T entity) where T : class, IEntity;

    }
}
