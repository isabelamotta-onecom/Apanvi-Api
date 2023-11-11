using PetsAdoption.Domain.Models;
using System.Reflection.Metadata.Ecma335;

namespace PetsAdoption.Api.Contracts.Requests;

public class AddUserRequest
{
    public Roles Role { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
