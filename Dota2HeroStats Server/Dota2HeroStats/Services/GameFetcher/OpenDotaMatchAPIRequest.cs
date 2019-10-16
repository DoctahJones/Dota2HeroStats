using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2HeroStats.Services.GameFetcher
{
    public class OpenDotaMatchAPIRequest : APIRequest
    {
        //https://api.opendota.com/api/matches/3669769733
        public OpenDotaMatchAPIRequest(IAPIRequestSender apiRequestSender, string matchId)
            : base(apiRequestSender)
        {
            SetUrl(matchId);
        }

        public void SetUrl(string matchId)
        {
            if (matchId == null)
            {
                throw new ArgumentNullException("No match id set.");
            }
            if (!matchId.All(Char.IsDigit))
            {
                throw new ArgumentException("Match id should be all digits.");
            }
            Url = "https://api.opendota.com/api/matches/" + matchId;
        }
    }
}
