using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopHub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopHub.Models.Context
{
    public class ShopHubContext :DbContext
    {
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ShopHubContext() : base(GetOptions()) { }
        public ShopHubContext(DbContextOptions<ShopHubContext> options)
            : base(options) { }

        private static DbContextOptions GetOptions()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), configuration.GetConnectionString("ShopHubConnection")).Options;
        }
    }
}
