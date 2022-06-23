using Payroll.Core.BaseModels;

namespace Payroll.Bl.DTOs
{
    public class ExamplePersonDTO: BaseEntityDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

    }
}