using MakerChecker.Entities.Base;
using MakerChecker.Entities.Enums;

namespace MakerChecker.Entities;

public class PostStaging : StagingEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public string Tags { get; set; }
    public Guid? PostId { get; set; }
    public string RejectReason { get; set; }
    public StageStatus StageStatus { get; set; }
}
