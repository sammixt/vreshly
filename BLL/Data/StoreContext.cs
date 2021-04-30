using System;
using BLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext()
        {

        }
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Brand> Brands { get; set; }

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
                                                Initial Catalog=Vreshly;
                                                Persist Security Info=False;
                                                User ID=sa;
                                                Password=Sammy1234;
                                                MultipleActiveResultSets=False;
                                                Encrypt=True;
                                                TrustServerCertificate=True;
                                                ");
            }
        }
    }
}
