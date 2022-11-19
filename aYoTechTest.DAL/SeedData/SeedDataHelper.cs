
using Microsoft.Extensions.DependencyInjection;

namespace aYoTechTest.DAL.SeedData
{
    public class SeedDataHelper
    {
        private readonly IServiceProvider _serviceProvider;
        public SeedDataHelper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public void RunSeed()
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                UnitConversionSeedData unitConvertsionSeed =
                    scope.ServiceProvider.GetRequiredService<UnitConversionSeedData>();
                DefaultUserSeedData defaultUserSeed = scope.ServiceProvider.GetRequiredService<DefaultUserSeedData>();

                unitConvertsionSeed.InitializeDb();
                defaultUserSeed.InitializeDatabase().GetAwaiter().GetResult();
            }
        }
    }
}
