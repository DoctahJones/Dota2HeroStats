using Dota2HeroStats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2HeroStats.Services.GameFetcher
{
    interface IGameFetcher
    {
        Task<AbilityDraftMatch> FetchGame(string matchId);
    }
}
