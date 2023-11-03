using AutoMapper;
using PetsAdoption.Api.Contracts.Responses;
using PetsAdoption.Domain.Models;

namespace PetsAdoption.Api.Mapping;

public class PetsAdoptionProfile : Profile
{
    public PetsAdoptionProfile()
    {
        CreateMap<Pet, PetResponse>();
        CreateMap<User, UserResponse>();
        CreateMap<Contact, ContactResponse>();
    }
}
