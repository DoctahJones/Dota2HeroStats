using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models
{
    public class HeroAbilityStat : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 1)]
        public virtual int HeroId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 2)]
        public virtual int AbilityId { get; set; }

        public virtual int Matches { get; set; }

        public virtual int Wins { get; set; }


        public virtual Ability Ability { get; set; }

        public virtual Hero Hero { get; set; }


        [NotMapped]
        public ModelEntityState EntityState { get; set; }

    }
}