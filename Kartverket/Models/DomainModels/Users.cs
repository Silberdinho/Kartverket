using Microsoft.AspNetCore.Identity;

namespace Kartverket.Models
{
    /// <summary>
    /// represents a user in the system with identity management
    /// </summary>
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
