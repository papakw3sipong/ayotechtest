using Microsoft.AspNetCore.Identity;

namespace aYoTechTest.Models.Entities.Identity
{
    public class aYoTechTestUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
