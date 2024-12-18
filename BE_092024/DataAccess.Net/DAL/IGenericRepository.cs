using System.Data.SqlClient;
using System.Linq.Expressions;

namespace DataAccess.Net.DAL;

public interface IGenericRepository<T> where T : class
{
   Task<List<T>> GetAll();
   Task<T> Insert(T entity);
   Task<T> Update(T entity);
   Task<T> Delete( T entity);
   Task<T> Search(int id);
}