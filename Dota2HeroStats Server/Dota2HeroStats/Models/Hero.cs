using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models
{
    public class Hero : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int HeroId { get; set; }
        public virtual string Name { get; set; }
        public virtual string LocalizedName { get; set; }
        public virtual string PrimaryAttr { get; set; }
        public virtual string AttackType { get; set; }
        public virtual int Legs { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<HeroAbilityStat> HeroAbilityStats { get; set; }

        [NotMapped]
        public ModelEntityState EntityState { get; set; }
    }
}