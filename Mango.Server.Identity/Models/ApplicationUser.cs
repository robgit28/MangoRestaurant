using Microsoft.AspNetCore.Identity;

namespace Mango.Server.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
    }
}
