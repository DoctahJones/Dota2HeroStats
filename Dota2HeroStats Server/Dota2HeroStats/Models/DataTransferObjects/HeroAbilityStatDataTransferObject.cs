using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models.DataTransferObjects
{
    public class HeroAbilityStatDataTransferObject
    {
        public long HeroId { get; set; }

        public int AbilityId { get; set; }

        public int Matches { get; set; }

        public int Wins { get; set; }

        public static HeroAbilityStatDataTransferObject CreateHeroAbilityStatDataTransferObject(HeroAbilityStat a)
        {
            return new HeroAbilityStatDataTransferObject
            {
                HeroId = a.HeroId,
                AbilityId = a.AbilityId,
                Matches = a.Matches,
                Wins = a.Wins
            };
        }

    }
}