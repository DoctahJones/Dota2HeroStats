using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models
{
    public class Player : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 1)]
        public virtual long MatchId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 2)]
        public virtual int PlayerSlot { get; set; }

        public virtual int? AccountId { get; set; }
        public virtual bool IsRadiant { get; set; }

        public virtual int? Item_0 { get; set; }
        public virtual int? Item_1 { get; set; }
        public virtual int? Item_2 { get; set; }
        public virtual int? Item_3 { get; set; }
        public virtual int? Item_4 { get; set; }
        public virtual int? Item_5 { get; set; }

        public virtual int? Backpack_0 { get; set; }
        public virtual int? Backpack_1 { get; set; }
        public virtual int? Backpack_2 { get; set; }

        public virtual int HeroLevel { get; set; }

        public virtual int Kills { get; set; }
        public virtual int Deaths { get; set; }
        public virtual int Assists { get; set; }

        public virtual int LastHits { get; set; }
        public virtual int Denies { get; set; }

        public virtual int GoldPerMin { get; set; }
        public virtual int XpPerMin { get; set; }

        public virtual int HeroDamage { get; set; }
        public virtual int HeroHealing { get; set; }
        public virtual int TowerDamage { get; set; }

        public virtual AbilityDraftMatch AbilityDraftMatch { get; set; }
        public virtual Hero Hero { get; set; }
        public virtual ICollection<Ability> Abilities { get; set; }



        [NotMapped]
        public ModelEntityState EntityState { get; set; }
    }
}