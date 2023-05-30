using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MakerChecker.Pages.Posts;

[Authorize(Roles = "SuperAdmin")]
public class ApproveModel : PageModel
{
    public void OnGet()
    {
    }
}
