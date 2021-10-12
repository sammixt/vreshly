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
        public DbSet<NewsLetterSubscription> NewsLetters { get; set; }
        public DbSet<RecurringOrder> RecurringOrders { get; set; }
        public DbSet<Message> Message { get; set; }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<AppRedis> Redis { get; set; }
        public DbSet<Educative> Educatives { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {


                optionsBuilder.UseSqlServer(@"Server=192.99.150.165;
                                                Initial Catalog=vreshlyc_Vreshly;
                                                Persist Security Info=False;
                                                User ID=bode;
                                                Password=P@$$w0rd;
                                                MultipleActiveResultSets=False;
                                                Encrypt=True;
                                                TrustServerCertificate=True;
                                                Connection Timeout=30;");

                //optionsBuilder.UseSqlServer(@"Server=localhost;
                //                                Initial Catalog=Vreshly;
                //                                Persist Security Info=False;
                //                                User ID=sa;
                //                                Password=Sammy1234;
                //                                MultipleActiveResultSets=False;
                //                                Encrypt=True;
                //                                TrustServerCertificate=True;
                //                                ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
