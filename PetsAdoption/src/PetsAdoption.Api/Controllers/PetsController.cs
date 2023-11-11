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
    private readonly IPicturesRepository _picturesRepository;

    public PetsController(IPetsRepository petsRepository, IMapper mapper, IContactsRepository contactsRepository, IPicturesRepository picturesRepository)
    {
        _petsRepository = petsRepository;
        _mapper = mapper;
        _contactsRepository = contactsRepository;
        _picturesRepository = picturesRepository;
    }

    [HttpPost]
    public async Task<ActionResult<PetResponse>> Add(AddPetRequest petRequest)
    {
        var pet = new Pet()
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

    [HttpPost("{id:guid}/pictures")]
    public async Task<ActionResult> AddPicture(Guid id, IFormFile file)
    {
        var pet = await _petsRepository.GetById(id);
        if (pet is null)
        {
            return NotFound();
        }

        var picture = new Picture()
        {
            Pet = pet,
            FileName = Path.GetFileName(file.FileName)
        };
      
        using var stream = new MemoryStream();
        file.CopyTo(stream);
        picture.File = stream.ToArray();

        await _picturesRepository.Add(picture);

        return NoContent(); // todo return created
    }

    [HttpGet("{petId:guid}/pictures/{pictureId:guid}")]
    public async Task<ActionResult> GetPicture(Guid petId, Guid pictureId)
    {
        var picture = await _picturesRepository.Get(petId, pictureId);
        if (picture is null)
        {
            return NotFound();
        }

        return File(picture.File, "application/octet-stream", picture.FileName);
    }

    [HttpPatch("{petId:guid}/pictures/{pictureId:guid}")]
    public async Task<ActionResult> Patch(Guid petId, Guid pictureId, PatchPictureRequests request)
    {
        var picture = await _picturesRepository.Get(petId, pictureId);
        if (picture is null)
        {
            return NotFound();
        }

        picture.IsCover = request.IsCover;

        await _picturesRepository.Update(picture);

        return NoContent();
    }
}
