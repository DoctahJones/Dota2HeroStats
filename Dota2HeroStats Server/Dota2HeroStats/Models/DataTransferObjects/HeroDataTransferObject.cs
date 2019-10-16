using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models.DataTransferObjects
{
    public class HeroDataTransferObject
    {
        public int HeroId { get; set; }
        public string Name { get; set; }
        public string LocalizedName { get; set; }
        public string PrimaryAttr { get; set; }
        public string AttackType { get; set; }
        public int Legs { get; set; }

        public ICollection<string> Roles { get; set; }

        public static HeroDataTransferObject CreateHeroDataTransferObject(Hero hero)
        {
            var x = new HeroDataTransferObject
                    {
                        HeroId = hero.HeroId,
                        Name = hero.Name,
                        LocalizedName = hero.LocalizedName,
                        PrimaryAttr = hero.PrimaryAttr,
                        AttackType = hero.AttackType,
                        Legs = hero.Legs,
                        Roles = hero.Roles == null ? new List<string>() : hero.Roles.Select(r => r.Name).ToList()
                    };
            return x;
        }

        public Hero ToHeroModel()
        {
            var hero = new Hero
            {
                HeroId = this.HeroId,
                Name = this.Name,
                LocalizedName = this.LocalizedName,
                PrimaryAttr = this.PrimaryAttr,
                AttackType = this.AttackType,
                Legs = this.Legs,
                Roles = new HashSet<Role>()
            };
            return hero;
        }

    }
}