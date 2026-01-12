using Aquila.Data.Presentation.Models;


namespace Aquila.Data.Presentation.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerViewModel> GetAll();
        CustomerViewModel? GetById(int id);
        void Add(CustomerViewModel customer);
        void Update(CustomerViewModel customer);
        void Delete(int id);
    }
}
