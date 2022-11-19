using aYoTechTest.Api.Controllers;
using aYoTechTest.BR.Classes;
using aYoTechTest.BR.Enums;
using aYoTechTest.BR.Services.Interfaces;
using aYoTechTest.BR.ViewModels;
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
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using System.Runtime.CompilerServices;

namespace aYoTechTest.ApiTest
{
    public class UnitConverterControllerTest
    {

        private const string _connectionString = "Server=localhost;Port=5432;User Id=admin;Password=admin;Database=aYoTechTestDb";

        private List<aYoTechTestUser> testUserList;
        private string _testUserId = Guid.NewGuid().ToString();
        private const string _testUserName = "testuser";
        private const string _testUserPass = "Test@123";

        private readonly IOptions<ApiSetting> _apiSetting;
        private readonly IConfigurationSection _configurationSection;


        private readonly UnitConverterController _conversionController;
        private readonly IdentityController _identityController;


        private readonly AppDataContext _context;
        private readonly IIdentityUserService _iuService;
        private readonly IUnitConvertionService _ucService;
        private readonly ICurrentUserHelper _currentUser;
        private readonly DbContextOptionsBuilder<AppDataContext> _dbOptionBuilder;

        private Dictionary<string, string> _testConfiguration;


        public UnitConverterControllerTest()
        {
            _dbOptionBuilder = new DbContextOptionsBuilder<AppDataContext>();
            _dbOptionBuilder.UseNpgsql(_connectionString);

            _currentUser = new CurrentUserHelper(new HttpContextAccessor());
            _currentUser.IsTestMode = true;
            _context = new AppDataContext(_dbOptionBuilder.Options, _currentUser);




            if (_testConfiguration == null)
                _testConfiguration = new Dictionary<string, string>()
                {
                    { "TokenSecret", "4LhsueRzcFKvoAyNV2PR0QPqQdUIZVqD" },
                    {"TokenIssuer", "Charles Yeboah Oppong" },
                    {"TokenAudience", "Charles Yeboah Oppong" },
                    {"AllowedUserNameChars", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" },
                    {"PasswordLength", "6" },
                    {"LockoutTimeInMinutes", "15" },
                    {"AllowdFailedAttempts", "3" },
                    { "ApiRunMode", "DEVELOPMENT" }
                };

            //var configBuilder = new ConfigurationBuilder()
            //    .AddInMemoryCollection(_testConfiguration).Build();


            _apiSetting = new Mock<IOptions<ApiSetting>>().Object;
            _configurationSection = new Mock<IConfigurationSection>().Object;

            var store = new Mock<UserManager<aYoTechTestUser>>();
            var userManager = TestUserHelper.MockUserManager<aYoTechTestUser>(TestUsers);


            _ucService = new UnitConversionService(_context);
            _iuService = new IdentityUserService(userManager.Object, _apiSetting);

            _conversionController = new UnitConverterController(_ucService);
            _identityController = new IdentityController(_iuService);

        }

        private List<aYoTechTestUser> TestUsers
        {
            get
            {
                if (testUserList == null)
                    testUserList = new List<aYoTechTestUser>()
                    {
                        new aYoTechTestUser()
                        {
                              Id = _testUserId,
                               FullName = _testUserName,
                               UserName = _testUserName,
                               Email = "test@example.com"
                        }
                    };

                return testUserList;
            }
        }


        private async Task<string> GetToken()
        {
            ApiAuthRequest _tokenRequest = new ApiAuthRequest()
            {
                Password = _testUserPass,
                Username = _testUserName
            };

            var _tokenResult = await _identityController.AuthenticateUser(_tokenRequest);
            return ((ApiAuthResponse)_tokenResult).Token;
        }

        public async Task A_WhenCalled_ReturnAccessToken()
        {
            //Act
            ApiAuthRequest _tokenRequest = new ApiAuthRequest()
            {
                Password = _testUserPass,
                Username = _testUserName
            };

            var _tokenResult = await _identityController.AuthenticateUser(_tokenRequest);
            Assert.NotNull(_tokenResult);
            var _token = Assert.IsType<ApiAuthResponse>(_tokenRequest);
            Assert.True(_token.FullName.Equals(_testUserName));
            Assert.True(!string.IsNullOrEmpty(_token.Token));

        }


        //public async Task A_WhenCalled_ConvertKm_To_Miles_Test()
        //{
        //    //Act
        //    var _conversionType = _context.SupportedConversions.Where(x => x.)

        //    ConvertUnitViewModel _convertData = new ConvertUnitViewModel()
        //    {
        //        ConversionType = ConversionType.Metric_To_Imperical,
        //        SourceUnitId = 1,
        //        TargetUnitId = 1,
        //        UnitValue = 4
        //    };



        //    var _conversionResult = await _conversionController.ConvertUnit(_convertData);
        //    Assert.NotNull(_conversionResult);

        //    var _token = Assert.IsType<ApiAuthResponse>(_tokenRequest);
        //    Assert.True(_token.FullName.Equals(_testUserName));
        //    Assert.True(!string.IsNullOrEmpty(_token.Token));

        //}

    }
}
