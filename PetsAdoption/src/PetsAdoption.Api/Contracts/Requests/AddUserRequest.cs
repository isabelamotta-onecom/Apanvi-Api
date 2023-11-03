using PetsAdoption.Domain.Models;
using System.Reflection.Metadata.Ecma335;

namespace PetsAdoption.Api.Contracts.Requests;

public class AddUserRequest
{
    public string Name { get; set; } = string.Empty;
    public Roles Role { get; set; }
}
