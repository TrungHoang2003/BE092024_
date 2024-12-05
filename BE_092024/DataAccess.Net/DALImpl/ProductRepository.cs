using DataAccess.Net.DAL;
using DataAccess.Net.DataObject;
using DataAccess.Net.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Net.DALImpl;

public class ProductRepository: GenericRepository<Product>, IProductRepository
{
    public ProductRepository(BE092024_DbContext context) : base(context)
    {
    }
}
