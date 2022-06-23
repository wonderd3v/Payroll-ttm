using Payroll.Model.Context;
using Payroll.Model.Repositories;

namespace Payroll.Model
{
    public interface IExamplePersonRepository: IBaseRepository<ExamplePerson> {}

    public class ExamplePersonRepository: BaseRepository<ExamplePerson>, IExamplePersonRepository
    {
        public ExamplePersonRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}