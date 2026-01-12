
using Aquila.Data.Presentation.Models;
namespace Aquila.Data.Presentation.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<OrderViewModel> GetOrdersWithUsers();
        void Add(OrderViewModel order);
    }
}
