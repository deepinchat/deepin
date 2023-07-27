using Microsoft.AspNetCore.Http;

namespace DeepIn.Service.Common.Services;

public class UserContext : IUserContext
{
    private IHttpContextAccessor _context;

    private string _userId;
    public UserContext(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public string UserId
    {
        get
        {
            if (string.IsNullOrEmpty(_userId))
            {
                _userId = _context.HttpContext.User.FindFirst("sub").Value;
            }
            return _userId;
        }
    }
}
