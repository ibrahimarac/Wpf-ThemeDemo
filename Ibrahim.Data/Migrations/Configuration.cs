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
            context.Users.AddOrUpdate(u => new { u.Name, u.Surname, u.Email },
                new User
                {
                    Id = 1,
                    Name = "Halil İbrahim",
                    Surname = "Araç",
                    Email = "yazilim@ibrahimarac.com"
                }
            );

            context.LoginUsers.AddOrUpdate(u => u.Username,
                new LoginUser
                {
                    UserId = 1,
                    Username = "ibrahim",
                    Password = "12345"
                }
            );

            context.Themes.AddOrUpdate(t => t.Name,
                new Theme { Name = "Silver",Id=1 },
                new Theme { Name = "Blue",Id=2 }
            );

            context.UserSettings.AddOrUpdate(u => u.UserId,
                new UserSettings
                {
                    UserId = 1,
                    ThemeId = 2
                }
            );

            

            context.SaveChanges();
        }
    }
}
