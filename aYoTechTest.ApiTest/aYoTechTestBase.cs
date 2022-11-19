using aYoTechTest.Api.Controllers;
using aYoTechTest.BR.Classes;
using aYoTechTest.BR.Services.Interfaces;
using aYoTechTest.CommonLibraries.Helpers;
using aYoTechTest.CommonLibraries.Interfaces;
using aYoTechTest.DAL.Classes;
using aYoTechTest.Models.Entities.Identity;
using aYoTechTest.Services.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace aYoTechTest.ApiTest
{
    public class aYoTechTestBase : IDisposable
    {

        public string _connectionString;

        public List<aYoTechTestUser> testUserList;
        public string _testUserId = Guid.NewGuid().ToString();
        public string _testUserName = "testuser";
        public string _testUserPass = "Test@123";

        public ApiSetting _apiSetting;
        public IConfigurationSection _configurationSection;

        public UserManager<aYoTechTestUser> _userManager;

        private IConfigurationRoot _configuration;

        public UnitConverterController _conversionController;
        public IdentityController _identityController;


        public AppDataContext _context;
        public IdentityUserContext _userContext;
        public IIdentityUserService _iuService;
        public IUnitConvertionService _ucService;
        public ICurrentUserHelper _currentUser;
        public DbContextOptionsBuilder<AppDataContext> _dbOptionBuilder;
        public DbContextOptionsBuilder<IdentityUserContext> _idbOptionBuilder;

        public Dictionary<string, string> _testConfiguration;
        ServiceCollection services = new ServiceCollection();

        public aYoTechTestBase()
        {
            _configuration = new ConfigurationBuilder()
              .AddJsonFile("testsettings.json").Build();

            _connectionString = _configuration.GetConnectionString("AyoDbConnection");
            _currentUser = new CurrentUserHelper(new HttpContextAccessor());
            _currentUser.IsTestMode = true;

            _dbOptionBuilder = new DbContextOptionsBuilder<AppDataContext>();
            _dbOptionBuilder.UseNpgsql(_connectionString);

            _idbOptionBuilder = new DbContextOptionsBuilder<IdentityUserContext>();
            _idbOptionBuilder.UseNpgsql(_connectionString);


            var mockUserManager = TestUserHelper.MockUserManager<aYoTechTestUser>(new List<aYoTechTestUser>
            {
                TestUser
            });

            _context = new AppDataContext(_dbOptionBuilder.Options, _currentUser);
            _userContext = new IdentityUserContext(_idbOptionBuilder.Options);

            _userManager = mockUserManager.Object;

            var _apiSettingString = _configuration.GetSection("ApiSetting");
            services.Configure<ApiSetting>(_apiSettingString);

            _apiSetting = _apiSettingString.Get<ApiSetting>();

            _ucService = new UnitConversionService(_context);
            _iuService = new IdentityUserService(_userManager, _apiSetting);
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public aYoTechTestUser TestUser
        {
            get
            {
                return new aYoTechTestUser { UserName = _testUserPass, Id = _testUserId, Email = "test@example.com", EmailConfirmed = true, FullName = _testUserName };
            }
        }




    }
}
