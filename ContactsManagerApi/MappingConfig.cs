using AutoMapper;
using ContactsManagerApi.Models;
using ContactsManagerApi.Models.DTO;

namespace ContactsManagerApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Contact, ContactsDTO>();
            CreateMap<ContactsDTO, Contact>();

            CreateMap<Contact, ContactsCreateDTO>().ReverseMap();
            CreateMap<Contact, ContactsUpdateDTO>().ReverseMap();

        }
    }
}
