using aYoTechTest.DAL.Classes;
using aYoTechTest.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace aYoTechTest.DAL.SeedData
{
    public class DefaultUserSeedData
    {
        private readonly AppDataContext _context;
        private readonly UserManager<aYoTechTestUser> _userManager;
        public DefaultUserSeedData(AppDataContext context,
                              UserManager<aYoTechTestUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task InitializeDatabase(
        )
        {
            _context.Database.EnsureCreated();

            if (await _userManager.FindByNameAsync("testuser") == null)
            {
                var user = new aYoTechTestUser
                {
                    UserName = "testuser",
                    Email = "testuser@test.com",
                    FullName = "Test User",
                    PhoneNumber = "+233246933136",
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddPasswordAsync(user, "Test@123");
                }
            }

        }
    }

}
