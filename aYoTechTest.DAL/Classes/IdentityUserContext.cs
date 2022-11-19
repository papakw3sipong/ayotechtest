using aYoTechTest.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aYoTechTest.DAL.Classes
{
    public class IdentityUserContext : IdentityDbContext<aYoTechTestUser, IdentityRole, string>
    {
        public IdentityUserContext(DbContextOptions<IdentityUserContext> options) : base(options)
        {
        }

    }
}
