using MakerChecker.Data;
using MakerChecker.Entities.Enums;
using MakerChecker.Services.CurrentUserServices;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MakerChecker.Pages.Posts;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public IndexModel(ApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    [BindProperty]
    public List<StagingPost> StagingPosts { get; set; }

    [BindProperty]
    public List<Post> Posts { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Posts = new List<Post>();
        StagingPosts = new List<StagingPost>();

        var posts = await _context.Posts.ToListAsync();
        foreach (var post in posts)
        {
            var item = post.Adapt<Post>();
            Posts.Add(item);
            /*
            Posts.Add(new Post
            {
                Title = post.Title,
                Description = post.Description,
                ShortDescription = post.ShortDescription,
                Tags = post.Tags,
                ModifiedBy = post.ModifiedBy,
                InsertedBy = post.InsertedBy,
                ApprovedBy = post.ApprovedBy
            });
            */
        }

        var stagedposts = await _context.PostStagings
            .Where(sp => sp.StageStatus == StageStatus.NeedApproval)
            .ToListAsync();
        foreach (var post in stagedposts)
        {
            var stagingPost = post.Adapt<StagingPost>();
            StagingPosts.Add(stagingPost);
            /*
            StagingPosts.Add(new StagingPost
            {
                Id = post.Id,
                Title = post.Title,
                PostId = post.PostId,
                Description = post.Description,
                InsertedBy = post.InsertedBy,
                InsertedTime = post.InsertedTime,
                ShortDescription = post.ShortDescription,
                Tags = post.Tags,
                CreateStatus = post.CreateStatus
            });
            */
        }
        return Page();
    }

    public async Task<IActionResult> OnPostApprovePostAsync(Guid id)
    {
        try
        {
            var stagingPost = await _context.PostStagings.FindAsync(id);

            var post = stagingPost.Adapt<Entities.Post>();
            post.ApprovedBy = _currentUserService.GetCurrentUserEmail();
            post.ApprovedTime = DateTime.UtcNow;

            /*
            Entities.Post post = new Entities.Post
            {
                Title = stagingPost.Title,
                Description = stagingPost.Description,
                ShortDescription = stagingPost.ShortDescription,
                Tags = stagingPost.Tags,
                ApprovedBy = _currentUserService.GetCurrentUserEmail(),
                InsertedBy = stagingPost.InsertedBy,
                InsertedTime = stagingPost.InsertedTime,
                ApprovedTime = DateTime.UtcNow,
            };
            */

            await _context.Posts.AddAsync(post);

            stagingPost.PostId = post.Id;
            stagingPost.StageStatus = StageStatus.Approved;

            await _context.SaveChangesAsync();

            return new JsonResult("Success");
        }catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new JsonResult("Failed");
        }
    }
}

public class Post
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public string Tags { get; set; }
    public string InsertedBy { get; set; }
    public string ModifiedBy { get; set; }
    public string ApprovedBy { get; set; }
}

public class StagingPost
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public string Tags { get; set; }
    public Guid? PostId { get; set; }
    public string InsertedBy { get; set; }
    public DateTime InsertedTime { get; set; }
    public CreateStatus CreateStatus { get; set; }
}
