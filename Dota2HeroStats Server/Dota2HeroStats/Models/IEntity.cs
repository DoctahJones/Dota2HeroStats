using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models
{
    public interface IEntity
    {
        ModelEntityState EntityState { get; set; }
    }

    public enum ModelEntityState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}