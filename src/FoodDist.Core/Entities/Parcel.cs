namespace FoodDist.Core.Entities;

public class Parcel
{
  public Guid Id { get; set; }
  public string BeneficiaryPhone { get; set; }
  public string Status { get; set; } = "Created";

}
