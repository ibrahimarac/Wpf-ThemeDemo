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
            //Veritabanına varsayılan değerleri ekle

            context.Users.AddOrUpdate(u => new { u.Name, u.Surname, u.Email },
                new User
                {
                    Id = 1,
                    Name = "Halil İbrahim",
                    Surname = "Araç",
                    Email = "yazilim@ibrahimarac.com"
                },
                new User
                {
                    Id = 2,
                    Name = "Osman",
                    Surname = "Dede",
                    Email = "osman@test.com"
                }
            );

            context.LoginUsers.AddOrUpdate(u => u.Username,
                new LoginUser
                {
                    UserId = 1,
                    Username = "ibrahim",
                    Password = "12345"
                },
                new LoginUser
                {
                    UserId = 2,
                    Username = "osman",
                    Password = "12345"
                }
            );

            context.Themes.AddOrUpdate(t => t.Name,
                new Theme { Name = "Silver",Id=1 },
                new Theme { Name = "Blue",Id=2 },
                new Theme { Name="Red",Id=3}
            );

            context.UserSettings.AddOrUpdate(u => u.UserId,
                new UserSettings
                {
                    UserId = 1,
                    ThemeId = 2
                },
                new UserSettings
                {
                    UserId = 2,
                    ThemeId = 3
                }
            );

            

            context.SaveChanges();
        }
    }
}
