namespace TARge25Shop.Models.Spaceship;

public class ImageViewModel
{
    public string FilePath { get; set; } =  String.Empty;
    public Guid ImageId { get; set; }
    public Guid? SpaceshipId { get; set; }
}