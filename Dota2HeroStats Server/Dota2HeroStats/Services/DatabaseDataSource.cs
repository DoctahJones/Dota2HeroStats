using Dota2HeroStats.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Dota2HeroStats.Services
{
    public class DatabaseDataSource : Dota2HeroStats.Services.IDataSource
    {

        public async Task<Hero> LookupHero(int heroId)
        {
            Hero hero = null;
            using (var db = new Dota2HeroStatsDB())
            {
                hero = await db.Heroes.AsNoTracking().FirstOrDefaultAsync(h => h.HeroId == heroId);
            }
            return hero;
        }

        public async Task<Role> LookupRoleByName(string stringRole)
        {
            Role role = null;
            using (var db = new Dota2HeroStatsDB())
            {
                role = await db.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Name == stringRole);
            }
            return role;
        }

        public async Task<Ability> LookupAbility(int abilityId)
        {
            using (var db = new Dota2HeroStatsDB())
            {
                return await db.Abilities.FindAsync(abilityId);
            }
        }

        public async Task<HeroAbilityStat> LookupHeroAbilityStat(int heroId, int abilityId)
        {
            using (var db = new Dota2HeroStatsDB())
            {
                return await db.HeroAbilityStats.FindAsync(heroId, abilityId);
            }
        }

        public async Task AddEntity<T>(T entity) where T : class, IEntity
        {
            await UpdateEntity(entity);
        }

        public async Task UpdateEntity<T>(T entity) where T : class, IEntity
        {
            using (var db = new Dota2HeroStatsDB())
            {
                //db.Set<T>().Add(entity);
                db.Set<T>().Attach(entity);
                foreach (DbEntityEntry<IEntity> entry in db.ChangeTracker.Entries<IEntity>())
                {
                    IEntity currEntity = entry.Entity;
                    entry.State = GetEntityState(currEntity.EntityState);
                }
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    throw new DataSourceException("Error Updating item in database.", e);
                }
            }
        }

        public async Task RemoveEntity<T>(T entity) where T : class, IEntity
        {
            await UpdateEntity(entity);
        }

        protected static EntityState GetEntityState(ModelEntityState entityState)
        {
            switch (entityState)
            {
                case ModelEntityState.Unchanged:
                    return EntityState.Unchanged;
                case ModelEntityState.Added:
                    return EntityState.Added;
                case ModelEntityState.Modified:
                    return EntityState.Modified;
                case ModelEntityState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Detached;
            }
        }

    }
}