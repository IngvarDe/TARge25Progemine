namespace TARge25Shop.Core.Dto;

public class RealEstateDto
{
    public Guid? Id { get; set; }
    public double? AreaCode { get; set; }
    public string Location { get; set; }
    public int NrOfRooms { get; set; }
    public string BuildingType { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}