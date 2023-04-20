using Scenario.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Scenario.Data.EFCore
{
    public class DataContext : DbContext
    {

        //public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new CustomerMap());
            //modelBuilder.ApplyConfiguration(new OrderMap());
            //modelBuilder.ApplyConfiguration(new OrderItemMap());
            //modelBuilder.ApplyConfiguration(new ProductMap());
            //modelBuilder.ApplyConfiguration(new StockMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=scenario;uid=sa;pwd=123456;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Customer> Customers { get; set; }
	}
}