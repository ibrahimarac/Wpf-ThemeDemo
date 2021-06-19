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
                    Name = "Halil İbrahim",
                    Surname = "Araç",
                    Email = "yazilim@ibrahimarac.com"
                }
            );

            context.LoginUsers.AddOrUpdate(u => u.Username,
                new LoginUser
                {
                   Username="ibrahim",
                   Password="12345"
                }
            );

            context.UserSettings.AddOrUpdate(u => u.UserId,
                new UserSettings
                {
                    UserId = 1,
                    ThemeId = 2
                }
            );

            context.Themes.AddOrUpdate(t => t.Name,
                new Theme { Name="Blue"},
                new Theme { Name="Silver"}
            );

            context.SaveChanges();
        }
    }
}
