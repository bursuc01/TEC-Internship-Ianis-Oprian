using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;
using AutoMapper;

namespace ApiApp.DataAccessLayer.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<PersonCreation, Person>().ReverseMap();
            CreateMap<SalaryInformation, Salary>().ReverseMap();
        }
    }
}
