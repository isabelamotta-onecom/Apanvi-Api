using PetsAdoption.Api.Contracts.Responses;
using PetsAdoption.Domain.Models;

namespace PetsAdoption.Api.Services.Abstractions;

public interface ITokenService
{
    AccessTokenResponse CreateToken(User user);
}
