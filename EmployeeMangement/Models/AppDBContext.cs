using EmployeeMangement.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Models
{
    public class AppDBContext:IdentityDbContext<ApplicationUser>
    {

        public AppDBContext(DbContextOptions<AppDBContext>options)
            :base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// this function is used to seed data
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Initialize is extention method used to add data to employee   
            modelBuilder.Initialize();

            //to edit behvoir of delete cascad to no action
            foreach (var foreignkey in modelBuilder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
            {
                foreignkey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
