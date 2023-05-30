using System.Security.Claims;

namespace MakerChecker.Services.CurrentUserServices;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return new Guid(userId);
    }

    public string GetCurrentUserEmail()
    {
        return _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
    }
}
