using PetsAdoption.Domain.Models;

namespace PetsAdoption.Api.Contracts.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Roles Role { get; set; }
}
