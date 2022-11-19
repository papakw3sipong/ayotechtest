using aYoTechTest.CommonLibraries.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;

namespace aYoTechTest.CommonLibraries.Helpers
{
    public class CurrentUserHelper : ICurrentUserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public bool IsTestMode { get; set; }
        public CurrentUserHelper(
            IHttpContextAccessor httpContextAccessor
            )
        {
            _httpContextAccessor = httpContextAccessor;
        }


        private string GetLocalIPAddress()
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                try
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    localIP = endPoint.Address.ToString();
                    return localIP;
                }
                catch (System.Exception e)
                {
                    e.Data.Clear();
                    return "127.0.0.1";
                }

            }
        }

        public string IpAddress()
        {
            if (IsTestMode)
                return GetLocalIPAddress();

            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }


        public string UserId()
        {
            if (_httpContextAccessor?.HttpContext?.User != null)
            {
                if (_httpContextAccessor?.HttpContext?.User?.Identity != null)
                {
                    if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                    {
                        var userIdClaim = _httpContextAccessor?.HttpContext?.User?.Claims?.SingleOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier));
                        if (userIdClaim != null)
                            return userIdClaim.Value;
                    }
                }
            }

            if (IsTestMode)
                return Guid.NewGuid().ToString();

            return string.Empty;
        }

        public string UserName()
        {
            if (_httpContextAccessor?.HttpContext?.User != null)
                if (_httpContextAccessor?.HttpContext?.User?.Identity != null)
                    if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                        return _httpContextAccessor.HttpContext.User.Identity.Name;


            if (IsTestMode)
                return "TestUser";

            return string.Empty;
        }

        public string FullName()
        {
            if (_httpContextAccessor?.HttpContext?.User != null)
                if (_httpContextAccessor?.HttpContext?.User?.Identity != null)
                    if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                    {
                        var userIdClaim = _httpContextAccessor?.HttpContext?.User?.Claims?.SingleOrDefault(x => x.Type.Equals(ClaimTypes.WindowsAccountName));
                        if (userIdClaim != null)
                            return userIdClaim.Value;
                    }


            if (IsTestMode)
                return "Test User";

            return string.Empty;
        }
    }
}
