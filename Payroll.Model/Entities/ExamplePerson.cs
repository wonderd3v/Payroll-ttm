using Payroll.Core.BaseModels;

namespace Payroll.Model
{
    public class ExamplePerson: BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}