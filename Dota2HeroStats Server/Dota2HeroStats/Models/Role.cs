using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models
{
    public class Role : IEntity
    {
        public virtual int RoleId { get; set; }

        public virtual string Name { get; set; }


        public virtual ICollection<Hero> Heroes { get; set; }

        [NotMapped]
        public ModelEntityState EntityState { get; set; }
    }
}