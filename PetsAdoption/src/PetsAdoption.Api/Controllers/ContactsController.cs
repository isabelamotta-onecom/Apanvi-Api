using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetsAdoption.Api.Contracts.Requests;
using PetsAdoption.Api.Contracts.Responses;
using PetsAdoption.Domain.Models;
using PetsAdoption.Infrastructure.Repositories.Abstractions;

namespace PetsAdoption.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactsRepository _contactsRepository;
    private readonly IMapper _mapper;
    private readonly IPetsRepository _petsRepository;

    public ContactsController(IContactsRepository contactsRepository, IMapper mapper, IPetsRepository petsRepository)
    {
        _contactsRepository = contactsRepository;
        _mapper = mapper;
        _petsRepository = petsRepository;
    }

    [HttpPost]
    public async Task<ActionResult<ContactResponse>> Add(AddContactRequest contactRequest)
    {
        var contact = new Contact
        {
            Id = Guid.NewGuid(),
            Name = contactRequest.Name,
            PhoneNumber = contactRequest.PhoneNumber
        };

        await _contactsRepository.Add(contact);

        var contactResponse = _mapper.Map<ContactResponse>(contact);

        return Created($"api/Contacts/{contact.Id}", contactResponse);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ContactResponse>> GetById(Guid id)
    {
        var contact = await _contactsRepository.GetById(id);
        if (contact is null)
        {
            return NotFound();
        }

        var contactResponse = _mapper.Map<ContactResponse>(contact);

        return Ok(contactResponse);
    }

    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetAll()
    {
        var contacts = await _contactsRepository.GetAll();

        var contactsResponse = _mapper.Map<List<ContactResponse>>(contacts);

        return Ok(contactsResponse);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var pets = await _petsRepository.GetByContact(id);
        if (pets is not null)
        {
            return BadRequest("Contact can not be deleted");
        }

        var contact = await _contactsRepository.GetById(id);
        if (contact is null)
        {
            return NotFound();
        }

        await _contactsRepository.Delete(contact);

        return NoContent();
    }
}
