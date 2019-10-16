using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models.DataTransferObjects
{
    public class AbilityDraftMatchDataTransferObject
    {

        public long MatchId { get; set; }

        public int StartTime { get; set; }
        public int DurationSeconds { get; set; }

        public int ServerCluster { get; set; }
        public int PatchNumber { get; set; }

        public int? SkillLevel { get; set; }
        public int GameMode { get; set; }


        public int DireKillScore { get; set; }
        public int RadiantKillScore { get; set; }
        public bool RadiantWin { get; set; }

        public ICollection<PlayerDataTransferObject> Players { get; set; }

        public static AbilityDraftMatchDataTransferObject CreateAbilityDraftMatchDataTransferObject(AbilityDraftMatch match)  {
            return new AbilityDraftMatchDataTransferObject
            {
                MatchId = match.MatchId,
                StartTime = match.StartTime,
                DurationSeconds = match.DurationSeconds,
                ServerCluster = match.ServerCluster,
                PatchNumber = match.PatchNumber,
                SkillLevel = match.SkillLevel,
                GameMode = match.GameMode,
                DireKillScore = match.DireKillScore,
                RadiantKillScore = match.RadiantKillScore,
                RadiantWin = match.RadiantWin,
                Players = match.Players.Select(p => PlayerDataTransferObject.CreatePlayerDataTransferObject(p)).ToList()
            };
        }



    }
}