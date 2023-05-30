using MakerChecker.Entities;
using MakerChecker.Entities.Base;
using MakerChecker.Services.CurrentUserServices;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MakerChecker.Data;

public class ApplicationDbContext : IdentityDbContext
{
    private readonly ICurrentUserService _currentUserService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        _currentUserService = this.GetService<ICurrentUserService>();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.InsertedTime = entry.Entity.InsertedTime == DateTime.MinValue ?
                        DateTime.UtcNow : entry.Entity.InsertedTime;
                    entry.Entity.InsertedBy =  entry.Entity.InsertedBy ?? _currentUserService.GetCurrentUserEmail();
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedTime = entry.Entity.ModifiedTime == DateTime.MinValue ?
                        DateTime.UtcNow : entry.Entity.ModifiedTime;
                    entry.Entity.ModifiedBy = entry.Entity.ModifiedBy ?? _currentUserService.GetCurrentUserEmail();
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<PostStaging> PostStagings { get; set; }
}