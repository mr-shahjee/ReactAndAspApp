using Microsoft.EntityFrameworkCore;
using ReactAndAspApp.Server.Models;
using System;

namespace ReactAndAspApp.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerType>().HasData(
                new CustomerType { Id = 1, Name = "Regular" },
                new CustomerType { Id = 2, Name = "Premium" },
                new CustomerType { Id = 3, Name = "Corporate" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Ali Khan",
                    CustomerTypeId = 1,
                    Address = "Street 12",
                    City = "Karachi",
                    State = "SD",
                    Zip = "74000",
                    Description = "Regular retail customer",
                    LastUpdated = new DateTime(2024, 1, 1)
                },
                new Customer
                {
                    Id = 2,
                    Name = "Ahmed Raza",
                    CustomerTypeId = 2,
                    Address = "Block A",
                    City = "Lahore",
                    State = "PB",
                    Zip = "54000",
                    Description = "Premium customer",
                    LastUpdated = new DateTime(2024, 1, 1)
                },
                new Customer
                {
                    Id = 3,
                    Name = "Sara Khan",
                    CustomerTypeId = 1,
                    Address = "Main Road",
                    City = "Islamabad",
                    State = "IS",
                    Zip = "44000",
                    Description = "Regular customer",
                    LastUpdated = new DateTime(2024, 1, 1)
                },
                new Customer
                {
                    Id = 4,
                    Name = "ABC Corporation",
                    CustomerTypeId = 3,
                    Address = "Business Center",
                    City = "Karachi",
                    State = "SD",
                    Zip = "74010",
                    Description = "Corporate account",
                    LastUpdated = new DateTime(2024, 1, 1)
                },
                new Customer
                {
                    Id = 5,
                    Name = "XYZ Pvt Ltd",
                    CustomerTypeId = 3,
                    Address = "Industrial Area",
                    City = "Faisalabad",
                    State = "PB",
                    Zip = "38000",
                    Description = "Corporate client",
                    LastUpdated = new DateTime(2024, 1, 1)
                }
            );
        }
    }
}
