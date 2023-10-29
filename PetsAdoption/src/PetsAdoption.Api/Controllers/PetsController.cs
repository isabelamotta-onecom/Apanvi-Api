using Microsoft.AspNetCore.Mvc;
using PetsAdoption.Domain.Models;
using PetsAdoption.Infrastructure.Repositories.Abstractions;

namespace PetsAdoption.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private readonly IPetsRepository _petsRepository;

    public PetsController(IPetsRepository petsRepository)
    {
        _petsRepository = petsRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Add(Pet pet)
    {
        await _petsRepository.Add(pet);
        return Created($"api/Pets/{pet.Id}", pet);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var pet = await _petsRepository.GetById(id);
        if (pet is null)
        {
            return NotFound();
        }
        return Ok(pet);
    }
}
