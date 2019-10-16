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

namespace Dota2HeroStats.Controllers
{
    public class AbilitiesController : ApiController
    {
        private Dota2HeroStatsDB db = new Dota2HeroStatsDB();

        // GET: api/Abilities
        public async Task<IHttpActionResult> GetAbilities()
        {
            var abilities = await db.Abilities.Include(a => a.OriginalAbilityOwner).Select(a => new AbilityDataTransferObject
            {
                AbilityId = a.AbilityId,
                Name = a.Name,
                Img = a.Img,
                IsUltimate = a.IsUltimate,
                OriginalAbilityOwnerId = a.OriginalAbilityOwner == null ? 0 : a.OriginalAbilityOwner.HeroId
            }).ToListAsync();

            return Ok(abilities);
        }

        // GET: api/Abilities/5
        [ResponseType(typeof(Ability))]
        public async Task<IHttpActionResult> GetAbility(int id)
        {
            Ability ability = await db.Abilities.Include(a => a.OriginalAbilityOwner).FirstOrDefaultAsync(a => a.AbilityId == id);
            if (ability == null)
            {
                return NotFound();
            }
            var dto = AbilityDataTransferObject.CreateAbilityDataTransferObject(ability);
            return Ok(dto);
        }

        // PUT: api/Abilities/5
        [Authorize]
        [ClaimsSteamIdAuthorization]
        [ResponseType(typeof(void))]

        public async Task<IHttpActionResult> PutAbility(int id, AbilityDataTransferObject ability)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ability.AbilityId)
            {
                return BadRequest();
            }

            var dataSourceAbility = await db.Abilities.Include(a => a.OriginalAbilityOwner).FirstOrDefaultAsync(a => a.AbilityId == id);
            if (dataSourceAbility == null)
            {
                return NotFound();
            }

            var attachedEntry = db.Entry(dataSourceAbility);
            attachedEntry.CurrentValues.SetValues(ability); //copy values from dto to data source object.

            if (ability.OriginalAbilityOwnerId == null || ability.OriginalAbilityOwnerId == 0)
            {//if passed in ability has no originalhero set.
                dataSourceAbility.OriginalAbilityOwner = null;
            }
            else
            { //there was a passed in originalabilityowner
                //check if the id of passed in ability is the same as one already in db
                var needToUpdate = dataSourceAbility.OriginalAbilityOwner == null ? true : dataSourceAbility.OriginalAbilityOwner.HeroId == ability.OriginalAbilityOwnerId;
                var dataSourcePassedInAbilityOriginalHero = await db.Heroes.FirstOrDefaultAsync(h => h.HeroId == ability.OriginalAbilityOwnerId);
                if (dataSourcePassedInAbilityOriginalHero != null)
                {
                    dataSourceAbility.OriginalAbilityOwner = dataSourcePassedInAbilityOriginalHero;
                }
                else
                {
                    return BadRequest("An original ability owner with that ID could not be found.");
                }
            }

            db.Entry(dataSourceAbility).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbilityExists(id))
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

        // POST: api/Abilities
        [Authorize]
        [ClaimsSteamIdAuthorization]
        [ResponseType(typeof(Ability))]
        public async Task<IHttpActionResult> PostAbility(AbilityDataTransferObject ability)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var modelAbility = ability.ToAbilityModel();

            if (ability.OriginalAbilityOwnerId == null || ability.OriginalAbilityOwnerId == 0)
            {//if passed in ability has no originalhero set.
                modelAbility.OriginalAbilityOwner = null;
            }
            else
            { //there was a passed in originalabilityowner
                //lookup hero id in database
                var dataSourcePassedInAbilityOriginalHero = await db.Heroes.FirstOrDefaultAsync(h => h.HeroId == ability.OriginalAbilityOwnerId);
                if (dataSourcePassedInAbilityOriginalHero != null)
                {
                    modelAbility.OriginalAbilityOwner = dataSourcePassedInAbilityOriginalHero;
                }
                else
                {
                    return BadRequest("An original ability owner with that ID could not be found.");
                }
            }

            db.Abilities.Add(modelAbility);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AbilityExists(ability.AbilityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ability.AbilityId }, ability);
        }

        // DELETE: api/Abilities/5
        [Authorize]
        [ClaimsSteamIdAuthorization]
        [ResponseType(typeof(Ability))]
        public async Task<IHttpActionResult> DeleteAbility(int id)
        {
            Ability ability = await db.Abilities.FindAsync(id);
            if (ability == null)
            {
                return NotFound();
            }

            db.Abilities.Remove(ability);
            await db.SaveChangesAsync();

            return Ok(ability);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AbilityExists(int id)
        {
            return db.Abilities.Count(e => e.AbilityId == id) > 0;
        }
    }
}