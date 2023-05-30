namespace MakerChecker.Entities.Base;

public class MakerCheckerEntity : BaseEntity
{
    public string? ApprovedBy { get; set; }
    public DateTime ApprovedTime { get; set; }
    public string? RejectedBy { get; set; }
    public DateTime RejectedTime { get; set; }
    public string? RejectReason { get; set; }
}
