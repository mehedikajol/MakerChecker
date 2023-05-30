namespace MakerChecker.Entities.Base;

public class StagingEntity
{
    public Guid Id { get; set; }
    public string InsertedBy { get; set; }
    public DateTime InsertedTime { get; set; }
}
