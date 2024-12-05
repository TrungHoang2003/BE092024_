using DataAccess.Net.DAL;
using DataAccess.Net.DataObject;
using DataAccess.Net.DBContext;

namespace DataAccess.Net.DALImpl;

public class OrderRepository: GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(BE092024_DbContext context) : base(context)
    {
    }
}