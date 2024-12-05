using DataAccess.Net.DAL;
using DataAccess.Net.DataObject;
using DataAccess.Net.DBContext;

namespace DataAccess.Net.DALImpl;

public class OrderDetailRepository: GenericRepository<OrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(BE092024_DbContext context) : base(context)
    {
    }
}