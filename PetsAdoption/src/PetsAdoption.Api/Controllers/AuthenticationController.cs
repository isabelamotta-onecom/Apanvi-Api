using Microsoft.AspNetCore.Mvc;
using PetsAdoption.Api.Contracts.Requests;
using PetsAdoption.Api.Services.Abstractions;
using PetsAdoption.Infrastructure.Repositories.Abstractions;

namespace PetsAdoption.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IUsersRepository _usersRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly ITokenService _tokenService;

    public AuthenticationController(IUsersRepository usersRepository, IEncryptionService encryptionService, ITokenService tokenService)
    {
        _usersRepository = usersRepository;
        _encryptionService = encryptionService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequest request)
    {
        var user = await _usersRepository.GetByUserName(request.Username);
        if (user is null)
        {
            return NotFound("User not found");
        }

        var encryptedPassword = _encryptionService.Encrypt(request.Password);
        if (encryptedPassword != user.Password)
        {
            return Unauthorized("Incorrect Password");
        }

        var token = _tokenService.CreateToken(user);

        return Ok(token);
    }
}
