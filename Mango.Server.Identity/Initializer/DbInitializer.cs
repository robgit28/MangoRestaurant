using System.Security.Claims;
using IdentityModel;
using Mango.Server.Identity.DbContexts;
using Mango.Server.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Mango.Server.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        // referenced in Program.cs 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            ApplicationDbContext db, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager
            )
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            // running for first time essentially 
            if (_roleManager.FindByNameAsync(Constants.Administrator).Result == null)
            {
                // create roles 
                _roleManager.CreateAsync(new IdentityRole(Constants.Administrator)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Constants.Customer)).GetAwaiter().GetResult();
            }
            else 
            {
                return; 
            }

            // create admin user 
            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "07744 33 55 66",
                Firstname = "Rob",
                LastName = "Admin"
            };
            // creates the user 
            _userManager.CreateAsync(adminUser, "Password123!").GetAwaiter().GetResult();
            // assign to admin role 
            _userManager.AddToRoleAsync(adminUser, Constants.Administrator).GetAwaiter().GetResult();

            // creates the user claims - AspNetUserClaims table in DB 
            var user1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                // all the claims we want to store
                new Claim(JwtClaimTypes.Name, adminUser.Firstname + " " + adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName, adminUser.Firstname),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                new Claim(JwtClaimTypes.Role, Constants.Administrator),
            }).Result;

            // create customer user 
            ApplicationUser customerUser = new ApplicationUser()
            {
                UserName = "customer@gmail.com",
                Email = "customer@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "07755 45 67 89",
                Firstname = "Dave",
                LastName = "Customer"
            };
            // creates the user 
            _userManager.CreateAsync(customerUser, "Password123!").GetAwaiter().GetResult();
            // assign to admin role 
            _userManager.AddToRoleAsync(customerUser, Constants.Customer).GetAwaiter().GetResult();

            // creates the user claims - AspNetUserClaims table in DB 
            var user2 = _userManager.AddClaimsAsync(customerUser, new Claim[]
            {
                // all the claims we want to store
                new Claim(JwtClaimTypes.Name, customerUser.Firstname + " " + customerUser.LastName),
                new Claim(JwtClaimTypes.GivenName, customerUser.Firstname),
                new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
                new Claim(JwtClaimTypes.Role, Constants.Customer),
            }).Result;
        }
    }
}