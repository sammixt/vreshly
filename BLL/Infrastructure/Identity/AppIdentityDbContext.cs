using System;
using BLL.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BLL.Infrastructure.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext()
        {
        }

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                //optionsBuilder.UseSqlServer(@"Server=localhost;
                //                                Initial Catalog=VreshlyUserIdentity;
                //                                Persist Security Info=False;
                //                                User ID=sa;
                //                                Password=Sammy1234;
                //                                MultipleActiveResultSets=False;
                //                                Encrypt=True;
                //                                TrustServerCertificate=True;");
                optionsBuilder.UseSqlServer(@"Server=192.99.150.165;
                                                Initial Catalog=vreshlyc_VreshlyIdentity;
                                                Persist Security Info=False;
                                                User ID=bode;
                                                Password=P@$$w0rd;
                                                MultipleActiveResultSets=False;
                                                Encrypt=True;
                                                TrustServerCertificate=True;
                                                Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
