using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Models
{
    public static class SeedData
    {
        public static void Initialize(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                
                new Employee() { Id=1,Name="asd",Department=Dep.@is,Email="asd@asd.com"}
                );  
        }

    }
}
