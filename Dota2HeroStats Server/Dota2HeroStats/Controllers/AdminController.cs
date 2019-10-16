using Dota2HeroStats.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Dota2HeroStats.Controllers
{
    public class AdminController : ApiController
    {

        // GET: api/Admin
        [Authorize]
        [ClaimsSteamIdAuthorization]
        public IHttpActionResult Get()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var id = claims.Where(c => string.Equals(c.Type, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).First().Value;
            string steamId = id.Replace("https://steamcommunity.com/openid/id/", "");
            return Ok(steamId);
        }



    }
}
