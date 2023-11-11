namespace PetsAdoption.Api.Contracts.Responses;

public class PetResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ContactResponse? Contact { get; set; }
    public List<PictureResponse> Pictures { get; set; } = new List<PictureResponse>();
}
