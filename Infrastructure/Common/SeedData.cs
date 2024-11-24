using Domain.Web.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class SeedData
    {

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new List<IdentityRole>
            {
                new IdentityRole {Name="ADMIN", NormalizedName="ADMIN"},
                new IdentityRole {Name="CUSTOMER", NormalizedName="CUSTOMER"}
            };

            foreach (var role in roles) 
            {
                if(!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }


        public static async Task SeedDataAsync(ApplicationDbContext _dbcontext)
        {
            if(!_dbcontext.Brand.Any())
            {
                await _dbcontext.AddRangeAsync
                    (
                    new Category
                    {
                        CategoryName="Laptop"
                    },

                    new Category
                    {
                        CategoryName = "Monitor"
                    },

                    new Category
                    {
                        CategoryName = "CPU"
                    },

                    new Category
                    {
                        CategoryName = "Pendrive"
                    },

                    new Category
                    {
                        CategoryName = "HardDisk"
                    });
                await _dbcontext.SaveChangesAsync();
            }
        }
    }
}
