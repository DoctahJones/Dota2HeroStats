using Dota2HeroStats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Dota2HeroStats.Services
{
    public class ClaimsSteamIdAuthorizationAttribute : AuthorizationFilterAttribute
    {

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {

            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (!principal.Identity.IsAuthenticated)
            {
                //actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }
            if (!(principal.HasClaim(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" && ClaimIdIsValid(x.Value))))
            {
                //actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            //User is Authorized, complete execution
            return Task.FromResult<object>(null);

        }

        private bool ClaimIdIsValid(string value)
        {
            string steamId = value.Replace("https://steamcommunity.com/openid/id/", "");

            using (var db = new Dota2HeroStatsDB())
            {
                var admin = db.Admins.Find(steamId);
                if (admin == null)
                {
                    return false;
                }
                return true;
            }

        }

    }
}