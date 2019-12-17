namespace MyMarket.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MyMarket.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ExampleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ExampleContext context)
        {
            AddUser(context);
            AddUserCashier(context);
        }

        private bool AddUserCashier(ExampleContext context)
        {

            IdentityResult identityResult;

            UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));

            var user = new IdentityUser()
            {
                UserName = "cashier"
            };
            if (userManager.FindByName(user.UserName) != null)
            {
                return true;
            }

            identityResult = userManager.Create(user, "12345");

            return identityResult.Succeeded;
        }

        private bool AddUser(ExampleContext context)
        {
            // Identity result objects handle results from non-select operations
            // on the user datastore
            IdentityResult identityResult;
            // UserManager handles the management of a certain type of User, and it
            // requires a UserStore to handle the actual access to the datastore
            UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
            // Create a new user as a POCO
            var user = new IdentityUser()
            {
                UserName = "admin"
            };
            // Check if the user already exists. If so, the user exists so report true
            if (userManager.FindByName(user.UserName) != null)
            {
                return true;
            }
            // Since the user does not exist, attempt to create the user
            identityResult = userManager.Create(user, "password");
            // Pass back the result of attempting to create the user
            return identityResult.Succeeded;
        }
    }
}
