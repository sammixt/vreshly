using System;
using System.Reflection;
using BLL.Entities;
using BLL.Entities.OrderAggregate;
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
        public DbSet<Product> Products { get; set; }
        public DbSet<Variable> Variables { get; set; }
        public DbSet<VariableDetail> VariableDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInformation> GetUserInformation { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<WishList> WishLists { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
