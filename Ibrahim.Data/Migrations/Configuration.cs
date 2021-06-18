namespace Ibrahim.Data.Migrations
{
    using Ibrahim.Data.Core.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Ibrahim.Data.Context.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Ibrahim.Data.Context.UserContext context)
        {
            context.LoginUsers.AddOrUpdate(u => u.Username,
                new LoginUser
                {
                   Username="ibrahim",
                   Password="12345"
                }
            );
            context.UserSettings.AddOrUpdate(u => u.Theme,
                new UserSettings
                {
                    Theme = "Silver"
                }
            ) ;
            context.Users.AddOrUpdate(u => new { u.Name, u.Surname,u.Email },
                new User
                {
                    Name="Halil İbrahim",
                    Surname="Araç",
                    Email="yazilim@ibrahimarac.com"
                }
            );
            context.SaveChanges();
        }
    }
}
