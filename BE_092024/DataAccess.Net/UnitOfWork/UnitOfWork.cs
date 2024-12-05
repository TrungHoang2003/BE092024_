using DataAccess.Net.DAL;
using DataAccess.Net.DBContext;

namespace DataAccess.Net.UnitOfWork;

public class UnitOfWork: IUnitOfWork
{
    public UnitOfWork(IOrderDetailRepository orderDetailRepository, IProductRepository products, IOrderRepository orders, BE092024_DbContext dbContext)
    {
        OrderDetailRepository = orderDetailRepository;
        Products = products;
        Orders = orders;
        _dbContext = dbContext;
    }

    private readonly BE092024_DbContext _dbContext;
    public IOrderDetailRepository OrderDetailRepository { get; set; }
    public IProductRepository Products { get; set; }
    public IOrderRepository Orders { get; set; }
    public async Task<int> SaveChangesAsync()
    {
       return await _dbContext.SaveChangesAsync(); 
    }
}