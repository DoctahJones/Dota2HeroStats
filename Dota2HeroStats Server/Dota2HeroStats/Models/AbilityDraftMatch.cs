using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models
{
    public class AbilityDraftMatch : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual long MatchId { get; set; }


        public virtual int StartTime { get; set; }
        public virtual int DurationSeconds { get; set; }

        public virtual int ServerCluster { get; set; }
        public virtual int PatchNumber { get; set; }

        public virtual int? SkillLevel { get; set; }
        public virtual int GameMode { get; set; }


        public virtual int DireKillScore { get; set; }
        public virtual int RadiantKillScore { get; set; }
        public virtual bool RadiantWin { get; set; }

        public virtual ICollection<Player> Players { get; set; }




        [NotMapped]
        public ModelEntityState EntityState { get; set; }
    }
}