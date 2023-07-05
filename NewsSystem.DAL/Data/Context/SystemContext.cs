using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.DAL
{
    public class SystemContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<News> News { get; set; }

        public SystemContext(DbContextOptions<SystemContext> options)
        : base(options)
        {

        }
    }
}
