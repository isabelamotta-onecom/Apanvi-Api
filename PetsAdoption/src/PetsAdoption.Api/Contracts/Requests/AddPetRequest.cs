namespace PetsAdoption.Api.Contracts.Requests;

public class AddPetRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
