
namespace aYoTechTest.CommonLibraries.Interfaces
{
    public interface ICurrentUserHelper
    {
        bool IsTestMode { get; set; }
        string UserId();
        string UserName();
        string IpAddress();
        string FullName();
    }
}
