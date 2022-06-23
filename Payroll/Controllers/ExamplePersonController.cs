using Payroll.Bl.DTOs;
using Payroll.Model;
using Payroll.Services.Services;

namespace Payroll.Controllers
{
    public class ExamplePersonController: BaseController<ExamplePerson, ExamplePersonDTO>
    {
        public ExamplePersonController(IExamplePersonService service) : base(service) {}
    }
}