
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using MyMarket.Models;
using MyMarket.Providers;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMarket
{
	public partial class Startup
	{
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static Func<UserManager<IdentityUser>> UserManagerFactory { get; set; }

        static Startup()
        {
            String PublicClientId = "self";
            UserManagerFactory = () => new UserManager<IdentityUser>(new UserStore<IdentityUser>(new ExampleContext()));
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
                AllowInsecureHttp = true
            };
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}