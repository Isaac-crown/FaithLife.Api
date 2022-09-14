using Microsoft.AspNetCore.Identity;

namespace FaithLife.Api.Model
{
    public class ApplicationUser : IdentityUser
    {

        public string Address { get; set; }
    }
}
