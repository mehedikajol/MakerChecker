using MakerChecker.Entities.Base;

namespace MakerChecker.Entities;

public class Post : MakerCheckerEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public string Tags { get; set; }
}
