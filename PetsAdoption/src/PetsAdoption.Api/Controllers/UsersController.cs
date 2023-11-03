using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetsAdoption.Api.Contracts.Requests;
using PetsAdoption.Api.Contracts.Responses;
using PetsAdoption.Domain.Models;
using PetsAdoption.Infrastructure.Repositories.Abstractions;

namespace PetsAdoption.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public UsersController(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<UserResponse>> Add(AddUserRequest userRequest)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = userRequest.Name,
            Role = userRequest.Role,
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
