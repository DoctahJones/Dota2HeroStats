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
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace Dota2HeroStats.Controllers
{
    public class HeroAbilityStatsController : ApiController
    {
        private Dota2HeroStatsDB db = new Dota2HeroStatsDB();

        // GET: api/HeroAbilityStats
        public async Task<IHttpActionResult> GetHeroAbilityStats()
        {
            var heroStats = await db.HeroAbilityStats.Select(hs =>
                new HeroAbilityStatDataTransferObject
                {
                    HeroId = hs.HeroId,
                    AbilityId = hs.AbilityId,
                    Matches = hs.Matches,
                    Wins = hs.Wins
                }).ToListAsync();
            return Ok(heroStats);
        }

        // GET: api/HeroAbilityStats?heroId=1
        [Route("api/HeroAbilityStats/{heroId}")]
        [HttpGet]
        [ResponseType(typeof(List<HeroAbilityStatDataTransferObject>))]
        public async Task<IHttpActionResult> GetHeroAbilityStat(long heroId)
        {
            //Check if there is a hero with that heroId first and if not return 404
            if (!HeroExists(heroId))
            {
                return NotFound();
            }

            //get the stats of all abilities when combined with this heroid and then return that collection.
            var abilityHeroStats = await db.HeroAbilityStats.Where(hs => hs.HeroId == heroId).Select(hs =>
                new HeroAbilityStatDataTransferObject
                {
                    HeroId = hs.HeroId,
                    AbilityId = hs.AbilityId,
                    Matches = hs.Matches,
                    Wins = hs.Wins
                }).ToListAsync();

            if (abilityHeroStats == null)
            {
                return NotFound();
            }
            return Ok(abilityHeroStats);
        }

        // PUT: api/HeroAbilityStats/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutHeroAbilityStat(long id, HeroAbilityStat heroAbilityStat)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != heroAbilityStat.HeroId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(heroAbilityStat).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!HeroAbilityStatExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/HeroAbilityStats
        //[ResponseType(typeof(HeroAbilityStat))]
        //public async Task<IHttpActionResult> PostHeroAbilityStat(HeroAbilityStat heroAbilityStat)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.HeroAbilityStats.Add(heroAbilityStat);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (HeroAbilityStatExists(heroAbilityStat.HeroId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = heroAbilityStat.HeroId }, heroAbilityStat);
        //}

        //// DELETE: api/HeroAbilityStats/5
        //[ResponseType(typeof(HeroAbilityStat))]
        //public async Task<IHttpActionResult> DeleteHeroAbilityStat(long id)
        //{
        //    HeroAbilityStat heroAbilityStat = await db.HeroAbilityStats.FindAsync(id);
        //    if (heroAbilityStat == null)
        //    {
        //        return NotFound();
        //    }

        //    db.HeroAbilityStats.Remove(heroAbilityStat);
        //    await db.SaveChangesAsync();

        //    return Ok(heroAbilityStat);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HeroExists(long id)
        {
            return db.Heroes.Count(e => e.HeroId == id) > 0;
        }
    }
}