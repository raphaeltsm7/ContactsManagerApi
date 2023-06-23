using AutoMapper;
using ContactsManagerWeb.Models.DTO;

namespace ContactsManagerWeb
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ContactsDTO, ContactsCreateDTO>().ReverseMap();
            CreateMap<ContactsDTO, ContactsUpdateDTO>().ReverseMap();

        }
    }
}
