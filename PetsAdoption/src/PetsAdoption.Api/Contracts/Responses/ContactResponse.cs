namespace PetsAdoption.Api.Contracts.Responses;

public class ContactResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
