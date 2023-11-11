using PetsAdoption.Domain.Models;

namespace PetsAdoption.Api.Contracts.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public Roles Role { get; set; }
    public string UserName { get; set; } = string.Empty;
}
