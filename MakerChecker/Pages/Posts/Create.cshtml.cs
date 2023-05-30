using MakerChecker.Data;
using MakerChecker.Entities;
using MakerChecker.Entities.Enums;
using MakerChecker.Services.CurrentUserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MakerChecker.Pages.Posts;

[Authorize(Roles = "User")]
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateModel(ApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    [BindProperty]
    public PostCreateModel PostModel { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _context.PostStagings.AddAsync(new PostStaging
        {
            Title = PostModel.Title,
            Description = PostModel.Description,
            ShortDescription = PostModel.ShortDescription,
            Tags = PostModel.Tags,
            InsertedTime = DateTime.UtcNow,
            StageStatus = StageStatus.NeedApproval,
            CreateStatus = CreateStatus.Created,
            InsertedBy = _currentUserService.GetCurrentUserEmail()
        });
        await _context.SaveChangesAsync();
        return RedirectToPage(nameof(Index));
    }
}

public class PostCreateModel
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string ShortDescription { get; set; }

    [Required]
    public string Tags { get; set; }
}