using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models
{
    [DebuggerDisplay("ID: {AbilityId}, Name: {Name}")]
    public class Ability : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int AbilityId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Img { get; set; }

        public virtual bool IsUltimate { get; set; }

        public virtual Hero OriginalAbilityOwner { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public virtual ICollection<HeroAbilityStat> HeroAbilityStats { get; set; }

        [NotMapped]
        public ModelEntityState EntityState { get; set; }
    }
}