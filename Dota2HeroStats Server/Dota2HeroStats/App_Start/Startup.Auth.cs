using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using Owin;
using Owin.Security.Providers.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.App_Start
{
    public partial class Startup
    {
        private void ConfigureAuth(IAppBuilder app)
        {
            var cookieOptions = new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Account/Login")
            };

            app.UseCookieAuthentication(cookieOptions);

            app.SetDefaultSignInAsAuthenticationType(cookieOptions.AuthenticationType);
            app.UseSteamAuthentication(SteamAPIKey);

            
        }
    }
}