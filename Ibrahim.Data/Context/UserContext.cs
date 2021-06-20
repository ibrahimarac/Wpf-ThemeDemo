using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ibrahim.Data.Core.Domain;

namespace Ibrahim.Data.Context
{
    public class UserContext:DbContext
    {
        public UserContext():base("UserConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<LoginUser> LoginUsers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserSettings> UserSettings { get; set; }

        public DbSet<Theme> Themes { get; set; }
    }
}
