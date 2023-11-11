using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetsAdoption.Api.Contracts.Responses;
using PetsAdoption.Api.Services.Abstractions;
using PetsAdoption.Api.Settings;
using PetsAdoption.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetsAdoption.Api.Services;

public class TokenService : ITokenService
{
    private readonly byte[] _token;

    public TokenService(IOptions<TokenSettings> tokenSettings)
    {
        _token = Encoding.ASCII.GetBytes(tokenSettings.Value.TokenKey);
    }
    public AccessTokenResponse CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(_token);

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: cred);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        var accessToken = new AccessTokenResponse()
        {
            AccessToken = jwt,
        };

        return accessToken;
    }
}
