using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Dota2HeroStats.Models;
using Dota2HeroStats.Models.DataTransferObjects;
using Dota2HeroStats.Services;
using System.Web.Http.Cors;

namespace Dota2HeroStats.Controllers
{
    public class HeroesController : ApiController
    {
        private readonly Dota2HeroStatsDB db = new Dota2HeroStatsDB();

        // GET: api/Heroes
        public IHttpActionResult GetHeroes()
        {
            var heroList = db.Heroes.Include(h => h.Roles).Select(h =>
                        new HeroDataTransferObject
                {
                    HeroId = h.HeroId,
                    Name = h.Name,
                    LocalizedName = h.LocalizedName,
                    PrimaryAttr = h.PrimaryAttr,
                    AttackType = h.AttackType,
                    Legs = h.Legs,
                    Roles = h.Roles.Select(r => r.Name).ToList()
                });
            return Ok(heroList);
        }

        // GET: api/Heroes/5
        [ResponseType(typeof(HeroDataTransferObject))]
        public async Task<IHttpActionResult> GetHero(int id)
        {
            Hero hero = await db.Heroes.Include(h => h.Roles).FirstOrDefaultAsync(h => h.HeroId == id);
            if (hero == null)
            {
                return NotFound();
            }
            var dto = HeroDataTransferObject.CreateHeroDataTransferObject(hero);
            return Ok(dto);
        }


        // PUT: api/Heroes/5
        [Authorize]
        [ClaimsSteamIdAuthorization]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHero(int id, HeroDataTransferObject hero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hero.HeroId)
            {
                return BadRequest();
            }

            var dataSourceHero = await db.Heroes.Include(h => h.Roles).FirstOrDefaultAsync(h => h.HeroId == id);
            if (dataSourceHero == null)
            {
                return NotFound();
            }

            //using attach stuff from https://blog.maskalik.com/entity-framework/2013/12/23/entity-framework-updating-database-from-detached-objects/
            //entity is already in the context
            var attachedEntry = db.Entry(dataSourceHero);
            attachedEntry.CurrentValues.SetValues(hero); //copy values from dto to data source object.

            var roleConverter = new RoleStringToModelConverter(ServiceLocator.GetInstance().GetService<IDataSource>());
            var roleModelSet = await roleConverter.CreateRoleList(hero.Roles);  //create role models from string list in dto.

            //get roles to add
            var rolesToAdd = GetRolesToAdd(dataSourceHero, roleModelSet);

            //get roles to remove
            var rolesToRemove = GetRolesToRemove(dataSourceHero, roleModelSet);

            //remove the roles we don't want
            foreach (Role curr in rolesToRemove)
            {
                dataSourceHero.Roles.Remove(curr);
            }

            //add the roles to the datasourceHero that are new.
            foreach (Role curr in rolesToAdd)
            {
                if (curr.EntityState == ModelEntityState.Unchanged)
                {
                    var dbRole = await db.Set<Role>().FindAsync(curr.RoleId);
                    dataSourceHero.Roles.Add(dbRole);
                }
                else
                {
                    dataSourceHero.Roles.Add(curr);
                }
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private static List<Role> GetRolesToRemove(Hero dataSourceHero, HashSet<Role> roleModelSet)
        {
            var rolesToRemove = new List<Role>();
            foreach (Role oldRole in dataSourceHero.Roles)
            {
                var found = roleModelSet.FirstOrDefault(r => r.RoleId == oldRole.RoleId);
                if (found == null)
                {
                    rolesToRemove.Add(oldRole);
                }
            }
            return rolesToRemove;
        }

        private static List<Role> GetRolesToAdd(Hero dataSourceHero, HashSet<Role> roleModelSet)
        {
            var rolesToAdd = new List<Role>();
            foreach (Role newRole in roleModelSet)
            {
                var found = dataSourceHero.Roles.FirstOrDefault(r => r.RoleId == newRole.RoleId); //does the toPopulate contain the item from the new list.
                if (found == null)
                {
                    rolesToAdd.Add(newRole); //add item if its not found else its already in so we do not need to do anythin.
                }
            }
            return rolesToAdd;
        }

        // POST: api/Heroes
        [Authorize]
        [ClaimsSteamIdAuthorization]
        [ResponseType(typeof(HeroDataTransferObject))]
        public async Task<IHttpActionResult> PostHero(HeroDataTransferObject hero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var modelHero = hero.ToHeroModel();  //create db model from DTO

            var roleConverter = new RoleStringToModelConverter(ServiceLocator.GetInstance().GetService<IDataSource>());
            var roleModelSet = await roleConverter.CreateRoleList(hero.Roles);  //create role models from string list in dto.

            foreach (Role curr in roleModelSet)
            {
                if (curr.EntityState == ModelEntityState.Unchanged)
                {
                    var dbRole = await db.Set<Role>().FindAsync(curr.RoleId); 
                    modelHero.Roles.Add(dbRole);
                }
                else
                {
                    modelHero.Roles.Add(curr);
                }
            }

            db.Heroes.Add(modelHero);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HeroExists(modelHero.HeroId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hero.HeroId }, hero);
        }

        // DELETE: api/Heroes/5
        [Authorize]
        [ClaimsSteamIdAuthorization]
        [ResponseType(typeof(Hero))]
        public async Task<IHttpActionResult> DeleteHero(int id)
        {
            Hero hero = await db.Heroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }

            db.Heroes.Remove(hero);
            await db.SaveChangesAsync();

            return Ok(hero);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HeroExists(int id)
        {
            return db.Heroes.Count(e => e.HeroId == id) > 0;
        }
    }
}