using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {  
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(CreateUserDataSeed());

            base.OnModelCreating(modelBuilder);
        }

        private User[] CreateUserDataSeed()
        {
            User[] users = [
                new User
                {
                    Id = 1,
                    FirstName = "Manuel",
                    LastName = "Cecarelli",
                    Email = "manu@gmail.com",
                    HashedPassword = "manu1234",
                    Role = UserType.Admin
                },
                new User
                {
                    Id = 2,
                    FirstName = "Federico",
                    LastName = "Perez",
                    Email = "fede@gmail.com",
                    HashedPassword = "fede1234",
                    Role = UserType.Employee
                },
                new User
                {
                    Id = 3,
                    FirstName = "Victoria",
                    LastName = "Lopez",
                    Email = "viky@gmail.com",
                    HashedPassword = "viky1234",
                    Role = UserType.Client
                }
            ];

            return users;
        }
    }
}
