using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetsAdoption.Api.Contracts.Requests;
using PetsAdoption.Api.Contracts.Responses;
using PetsAdoption.Domain.Models;
using PetsAdoption.Infrastructure.Repositories.Abstractions;

namespace PetsAdoption.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private readonly IPetsRepository _petsRepository;
    private readonly IMapper _mapper;
    private readonly IContactsRepository _contactsRepository;

    public PetsController(IPetsRepository petsRepository, IMapper mapper, IContactsRepository contactsRepository)
    {
        _petsRepository = petsRepository;
        _mapper = mapper;
        _contactsRepository = contactsRepository;
    }

    [HttpPost]
    public async Task<ActionResult<PetResponse>> Add(AddPetRequest petRequest)
    {
        var pet = new Pet
        {
            Id = Guid.NewGuid(),
            Name = petRequest.Name,
            Description = petRequest.Description
        };

        await _petsRepository.Add(pet);

        var petResponse = _mapper.Map<PetResponse>(pet);

        return Created($"api/Pets/{pet.Id}", petResponse);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PetResponse>> GetById(Guid id)
    {
        var pet = await _petsRepository.GetById(id);
        if (pet is null)
        {
            return NotFound();
        }

        var petResponse = _mapper.Map<PetResponse>(pet);

        return Ok(petResponse);
    }

    [HttpPut("{id:guid}/contacts")]
    public async Task<ActionResult<PetResponse>> UpdateContact(Guid id, UpdateContactsResquest request)
    {
        var pet = await _petsRepository.GetById(id);
        if (pet is null)
        {
            return NotFound("Pet not found");
        }
        
        var contact = await _contactsRepository.GetById(request.ContactId);
        if (contact is null)
        {
            return NotFound("Contact not found");
        }
        
        pet.Contact = contact;

        await _petsRepository.Update(pet);

        var petResponse = _mapper.Map<PetResponse>(pet);

        return Ok(petResponse);
    }

    [HttpGet]
    public async Task<ActionResult<List<Pet>>> GetAll()
    {
        var pets = await _petsRepository.GetAll();

        var petsResponse = _mapper.Map<List<PetResponse>>(pets);

        return Ok(petsResponse);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var pet = await _petsRepository.GetById(id);
        if (pet is null)
        {
            return NotFound();
        }

        await _petsRepository.Delete(pet);

        return NoContent();
    }
}
