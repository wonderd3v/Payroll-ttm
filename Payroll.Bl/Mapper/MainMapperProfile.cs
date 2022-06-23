using AutoMapper;
using Payroll.Bl.DTOs;
using Payroll.Model;

namespace Payroll.Bl.Mapper
{
    public class MainMapperProfile : Profile
    {
        public MainMapperProfile()
        {
            CreateMap<ExamplePersonDTO, ExamplePerson>();
            CreateMap<ExamplePerson, ExamplePersonDTO>();
        }
    }
}