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
using Dota2HeroStats.Services.GameFetcher;
using Dota2HeroStats.Services.Stats;

namespace Dota2HeroStats.Controllers
{
    public class AbilityDraftMatchesController : ApiController
    {
        private Dota2HeroStatsDB db = new Dota2HeroStatsDB();

        // GET: api/AbilityDraftMatches
        public IQueryable<AbilityDraftMatchDataTransferObject> GetAbilityDraftMatches()
        {
            var matches = GetLatestAbilityDraftMatches();
            return matches;
        }

        private IQueryable<AbilityDraftMatchDataTransferObject> GetLatestAbilityDraftMatches(int numberOfMatches = 20)
        {
            var matches = db.AbilityDraftMatches.OrderByDescending(a => a.MatchId).Take(numberOfMatches)
                .Include(m => m.Players.Select(p => p.Abilities))
                .Include(m => m.Players.Select(p => p.Hero.Roles))
                    .Select(match => new AbilityDraftMatchDataTransferObject
                    {
                        MatchId = match.MatchId,
                        StartTime = match.StartTime,
                        DurationSeconds = match.DurationSeconds,
                        ServerCluster = match.ServerCluster,
                        PatchNumber = match.PatchNumber,
                        SkillLevel = match.SkillLevel,
                        GameMode = match.GameMode,
                        DireKillScore = match.DireKillScore,
                        RadiantKillScore = match.RadiantKillScore,
                        RadiantWin = match.RadiantWin,
                        Players = match.Players.Select(p => new PlayerDataTransferObject
                        {
                            PlayerSlot = p.PlayerSlot,
                            AccountId = p.AccountId,
                            Item_0 = p.Item_0,
                            Item_1 = p.Item_1,
                            Item_2 = p.Item_2,
                            Item_3 = p.Item_3,
                            Item_4 = p.Item_4,
                            Item_5 = p.Item_5,
                            Backpack_0 = p.Backpack_0,
                            Backpack_1 = p.Backpack_1,
                            Backpack_2 = p.Backpack_2,
                            HeroLevel = p.HeroLevel,
                            Kills = p.Kills,
                            Deaths = p.Deaths,
                            Assists = p.Assists,
                            LastHits = p.LastHits,
                            Denies = p.Denies,
                            GoldPerMin = p.GoldPerMin,
                            XpPerMin = p.XpPerMin,
                            HeroDamage = p.HeroDamage,
                            HeroHealing = p.HeroHealing,
                            TowerDamage = p.TowerDamage,
                            Hero = new HeroDataTransferObject
                            {
                                HeroId = p.Hero.HeroId,
                                Name = p.Hero.Name,
                                LocalizedName = p.Hero.LocalizedName,
                                PrimaryAttr = p.Hero.PrimaryAttr,
                                AttackType = p.Hero.AttackType,
                                Legs = p.Hero.Legs,
                                Roles = p.Hero.Roles.Select(r => r.Name).ToList()
                            },
                            Abilities = p.Abilities.Select(a => new AbilityDataTransferObject
                            {
                                AbilityId = a.AbilityId,
                                Name = a.Name,
                                Img = a.Img,
                                IsUltimate = a.IsUltimate,
                                OriginalAbilityOwnerId = a.OriginalAbilityOwner.HeroId
                            }).ToList()
                        }).ToList()
                    });
            return matches;
        }

        // GET: api/AbilityDraftMatches/5
        [ResponseType(typeof(AbilityDraftMatchDataTransferObject))]
        public async Task<IHttpActionResult> GetAbilityDraftMatch(long id)
        {
            AbilityDraftMatch abilityDraftMatch = await db.AbilityDraftMatches.Include(m => m.Players.Select(p => p.Abilities))
                .Include(m => m.Players.Select(p => p.Hero.Roles)).SingleOrDefaultAsync(m => m.MatchId == id);
            if (abilityDraftMatch == null)
            {
                var gameFetcher = ServiceLocator.GetInstance().GetService<IGameFetcher>();
                try
                {
                    abilityDraftMatch = await gameFetcher.FetchGame(id.ToString());
                }
                catch (GameFetcherException e)
                {
                    return InternalServerError(e);
                }

                //if we did not get a match from our game fetcher than as far as we know it doesn't exist and return notfound
                if (abilityDraftMatch == null)
                {
                    return NotFound();
                }
                //add the fetched game to the database and update stats related to this match
                var matchDetailUpdater = new MatchDetailUpdater(ServiceLocator.GetInstance().GetService<IDataSource>(), ServiceLocator.GetInstance().GetService<IStatUpdater>());
                await matchDetailUpdater.UpdateDataSourceAndStats(abilityDraftMatch);
            }
            
            var dto = AbilityDraftMatchDataTransferObject.CreateAbilityDraftMatchDataTransferObject(abilityDraftMatch);
            return Ok(dto);
        }

        //// PUT: api/AbilityDraftMatches/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutAbilityDraftMatch(long id, AbilityDraftMatch abilityDraftMatch)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != abilityDraftMatch.MatchId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(abilityDraftMatch).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AbilityDraftMatchExists(id))
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

        //// POST: api/AbilityDraftMatches
        //[ResponseType(typeof(AbilityDraftMatch))]
        //public async Task<IHttpActionResult> PostAbilityDraftMatch(AbilityDraftMatch abilityDraftMatch)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.AbilityDraftMatches.Add(abilityDraftMatch);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (AbilityDraftMatchExists(abilityDraftMatch.MatchId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = abilityDraftMatch.MatchId }, abilityDraftMatch);
        //}

        //// DELETE: api/AbilityDraftMatches/5
        //[ResponseType(typeof(AbilityDraftMatch))]
        //public async Task<IHttpActionResult> DeleteAbilityDraftMatch(long id)
        //{
        //    AbilityDraftMatch abilityDraftMatch = await db.AbilityDraftMatches.FindAsync(id);
        //    if (abilityDraftMatch == null)
        //    {
        //        return NotFound();
        //    }

        //    db.AbilityDraftMatches.Remove(abilityDraftMatch);
        //    await db.SaveChangesAsync();

        //    return Ok(abilityDraftMatch);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AbilityDraftMatchExists(long id)
        {
            return db.AbilityDraftMatches.Count(e => e.MatchId == id) > 0;
        }
    }
}