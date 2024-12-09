using DataAccess.Net.DAL;
using DataAccess.Net.DBContext;

namespace DataAccess.Net.UnitOfWork;

public class UnitOfWork: IUnitOfWork
{
    public UnitOfWork(IOrderDetailRepository orderDetail, IProductRepository products, IOrderRepository orders, BE092024_DbContext dbContext)
    {
        OrderDetail = orderDetail;
        Products = products;
        Orders = orders;
        _dbContext = dbContext;
    }

    private readonly BE092024_DbContext _dbContext;
    public IOrderDetailRepository OrderDetail { get; set; }
    public IProductRepository Products { get; set; }
    public IOrderRepository Orders { get; set; }
    public async Task<int> SaveChangesAsync()
    {
       return await _dbContext.SaveChangesAsync(); 
    }
}