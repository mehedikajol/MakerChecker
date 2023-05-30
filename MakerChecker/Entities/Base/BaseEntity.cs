namespace MakerChecker.Entities.Base;

public class BaseEntity
{
    public Guid Id { get; set; }
    public string InsertedBy { get; set; }
    public DateTime InsertedTime { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime ModifiedTime { get; set; }
}
