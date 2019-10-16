using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models.DataTransferObjects
{
    public class PlayerDataTransferObject
    {
        public int PlayerSlot { get; set; }
        public int? AccountId { get; set; }

        public int? Item_0 { get; set; }
        public int? Item_1 { get; set; }
        public int? Item_2 { get; set; }
        public int? Item_3 { get; set; }
        public int? Item_4 { get; set; }
        public int? Item_5 { get; set; }

        public int? Backpack_0 { get; set; }
        public int? Backpack_1 { get; set; }
        public int? Backpack_2 { get; set; }

        public int HeroLevel { get; set; }

        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }

        public int LastHits { get; set; }
        public int Denies { get; set; }

        public int GoldPerMin { get; set; }
        public int XpPerMin { get; set; }

        public int HeroDamage { get; set; }
        public int HeroHealing { get; set; }
        public int TowerDamage { get; set; }

        public HeroDataTransferObject Hero { get; set; }
        public ICollection<AbilityDataTransferObject> Abilities { get; set; }

        public static PlayerDataTransferObject CreatePlayerDataTransferObject(Player p)
        {
            return new PlayerDataTransferObject
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
                Hero = HeroDataTransferObject.CreateHeroDataTransferObject(p.Hero),
                Abilities = p.Abilities.Select(a => AbilityDataTransferObject.CreateAbilityDataTransferObject(a)).ToList()
            };
        }
    }
}