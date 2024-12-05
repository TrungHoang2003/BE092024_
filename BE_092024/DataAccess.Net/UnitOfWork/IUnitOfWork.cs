using DataAccess.Net.DAL;
using DataAccess.Net.DBContext;

namespace DataAccess.Net.UnitOfWork;

public interface IUnitOfWork
{
    IOrderDetailRepository OrderDetailRepository { get; set; }
    IProductRepository Products { get; set; }
    IOrderRepository Orders { get; set; }
    
    Task<int> SaveChangesAsync();
}