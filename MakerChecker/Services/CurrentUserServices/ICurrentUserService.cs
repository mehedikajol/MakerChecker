namespace MakerChecker.Services.CurrentUserServices;

public interface ICurrentUserService
{
    Guid GetCurrentUserId();
    string GetCurrentUserEmail();
}
