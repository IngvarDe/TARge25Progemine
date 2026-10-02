namespace TARge25Shop.Models.RealEstate;

public class RealEstateIndexViewModel
{
    public Guid? Id { get; set; }
    public double? AreaCode { get; set; }
    public string Location { get; set; } = string.Empty;
    public int NrOfRooms { get; set; }
    public string BuildingType { get; set; } = string.Empty;

    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}