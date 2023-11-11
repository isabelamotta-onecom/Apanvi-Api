using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetsAdoption.Api.Contracts.Requests;
using PetsAdoption.Api.Contracts.Responses;
using PetsAdoption.Api.Services.Abstractions;
using PetsAdoption.Domain.Models;
using PetsAdoption.Infrastructure.Repositories.Abstractions;

namespace PetsAdoption.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IEncryptionService _encryptionService;

    public UsersController(IUsersRepository usersRepository, IMapper mapper, IEncryptionService encryptionService)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _encryptionService = encryptionService;
    }

    [HttpPost]
    public async Task<ActionResult<UserResponse>> Add(AddUserRequest userRequest)
    {
        // TODO: Remove Name from all
        // Check if user name exists
        // check password is not empty

        var users = await _usersRepository.GetAll();
        foreach (User u in users) 
        {
            if (u.UserName == userRequest.UserName)
            {
                return Unauthorized("Username already exists");
            }
        }

        if (userRequest.Password == null)
        {
            return Unauthorized("Password can not be empty");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Role = userRequest.Role,
            UserName = userRequest.UserName,
            Password = _encryptionService.Encrypt(userRequest.Password)
        };

        await _usersRepository.Add(user);

        var userResponse = _mapper.Map<UserResponse>(user);

        return Created($"api/Users/{user.Id}", userResponse);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponse>> GetById(Guid id)
    {
        var user = await _usersRepository.GetById(id);
        if (user is null)
        {
            return NotFound();
        }

        var userResponse = _mapper.Map<UserResponse>(user);

        return Ok(userResponse);
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAll()
    {
        var users = await _usersRepository.GetAll();

        var usersResponse = _mapper.Map<List<UserResponse>>(users);

        return Ok(usersResponse);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var user = await _usersRepository.GetById(id);
        if (user is null)
        {
            return NotFound();      
        }

        await _usersRepository.Delete(user);

        return NoContent();
    }
}
