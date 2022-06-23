using AutoMapper;
using FluentValidation;
using Payroll.Bl.DTOs;
using Payroll.Model;

namespace Payroll.Services.Services
{
    public interface IExamplePersonService : IBaseService<ExamplePerson, ExamplePersonDTO> { }

    public class ExamplePersonService : BaseService<ExamplePerson, ExamplePersonDTO>, IExamplePersonService
    {
        public ExamplePersonService(
            IExamplePersonRepository repository,
            IMapper mapper,
            IValidator<ExamplePersonDTO> validator) : base(repository, mapper, validator) { }
    }
}