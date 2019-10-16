using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models.DataTransferObjects
{
    public class AbilityDataTransferObject
    {
        public int AbilityId { get; set; }

        public string Name { get; set; }

        public string Img { get; set; }

        public bool IsUltimate { get; set; }

        public int? OriginalAbilityOwnerId { get; set; }


        public static AbilityDataTransferObject CreateAbilityDataTransferObject(Ability a)
        {
            return new AbilityDataTransferObject
            {
                AbilityId = a.AbilityId,
                Name = a.Name,
                Img = a.Img,
                IsUltimate = a.IsUltimate,
                OriginalAbilityOwnerId = a.OriginalAbilityOwner == null ? 0 : a.OriginalAbilityOwner.HeroId
            };
        }

        public Ability ToAbilityModel()
        {
            var ability = new Ability
            {
                AbilityId = this.AbilityId,
                Name = this.Name,
                Img = this.Img,
                IsUltimate = this.IsUltimate
            };
            return ability;
        }
    }
}