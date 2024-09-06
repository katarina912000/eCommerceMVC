using eCommerceUdemy.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ECommUtility;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommModels.Models;

namespace ECommDataAccess.DbInitializer
{
    public class DbInitializer : IdbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db
            ) { 
            _roleManager= roleManager;
            _userManager= userManager;
            _db= db;
        
        }
        public void Initialize()
        {
            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }

            }
            catch(Exception ex)
            {

            }
            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_User_Cust).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Cust)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                //if roles are not created, then we will create admin user as well


                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin5@gmail.com",
                    Email = "admin5@gmail.com",
                    Name = "Leo",
                    PhoneNumber = "6662",
                    StreetAddress = "d",
                    State = "45",
                    PostalCode = "44",
                    City = "@2"
                }, "Admin1*").GetAwaiter().GetResult();


                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin5@gmail.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();


            }


            return;
        }
    }
}
