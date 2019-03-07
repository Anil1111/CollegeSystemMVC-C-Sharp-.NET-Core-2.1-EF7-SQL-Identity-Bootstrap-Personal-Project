using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeSystemMVC.Data
{
    public class CollegeSystemDbSeeder
    {
        private ApplicationDbContext _ctx;
        private RoleManager<IdentityRole> _roleManager;

        public CollegeSystemDbSeeder(ApplicationDbContext ctx, RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            _roleManager = roleManager;
        }

        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Roles.Any())
            {
                var roles = new string[] { "Student", "Professor", "Staff" };

                foreach (string roleName in roles)
                {
                    var roleResult = _roleManager.CreateAsync(new IdentityRole(roleName));
                    roleResult.Wait();

                }
                _ctx.SaveChanges();


            }
        }
    }
}
