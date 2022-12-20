using Data.Dto.Dtos;

namespace GraphicInterface.Common
{
    public class SessionHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;    
        private readonly ISession _session; 

        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;    
            _session = _httpContextAccessor.HttpContext.Session; 
        }

        public UserDto? User => _session.Get<UserDto>("User");

        public bool IsUserLoggedIn => User != null;
    }
}