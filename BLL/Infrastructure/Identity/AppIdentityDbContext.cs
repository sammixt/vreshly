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

                //optionsBuilder.UseSqlServer(@"Server=tcp:brainballerserver.database.windows.net,1433;
                //                                Initial Catalog=IInvest;
                //                                Persist Security Info=False;
                //                                User ID=brainballeradmin;
                //                                Password=Br@inB@ller;
                //                                MultipleActiveResultSets=False;
                //                                Encrypt=True;
                //                                TrustServerCertificate=False;
                //                                Connection Timeout=30;");
                //optionsBuilder.UseSqlServer(@"Server=ESC2581\SQLEXPRESS;
                //                                Initial Catalog=IInvest;
                //                                Trusted_Connection=True;");
                optionsBuilder.UseSqlServer(@"Server=localhost;
                                                Initial Catalog=VreshlyUserIdentity;
                                                Persist Security Info=False;
                                                User ID=sa;
                                                Password=Sammy1234;
                                                MultipleActiveResultSets=False;
                                                Encrypt=True;
                                                TrustServerCertificate=True;
                                                ");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
