using System.Data;
using System.Data.SqlClient;
using Common.DbHelper;
using DataAccess.Net.DAL;
using DataAccess.Net.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Net.DALImpl;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, new()
{
    private readonly BE092024_DbContext _dbContext;

    protected GenericRepository(BE092024_DbContext context)
    {
        _dbContext = context;
    }

    public async Task<List<T>> GetAll()
    {
        /*
        var conn = new SqlServerConnection().DbConnect();
        var result = new List<T>();
        try
        {
            await using var cmd = new SqlCommand($"select *from [{typeof(T).Name}]", conn);
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var entity = EntityMapper.MapReaderToEntity<T>(reader);

                Console.WriteLine($"Mapped Entity: {typeof(T).Name}");
                result.Add(entity);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return result;
        */
       
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> Insert(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public Task<T> Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
       return Task.FromResult(entity);
    }

    public Task<T> Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.FromResult(entity);
    }

    public async Task<T> Search(int id)
    {
        /*var result = new T();
        var conn = new SqlServerConnection().DbConnect();
        try
        {
            await using var cmd = new SqlCommand($"search_byID_" + $"{typeof(T).Name}", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id); 
            var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var entity = EntityMapper.MapReaderToEntity<T>(reader);
                result = entity;
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return result;*/
        
        var result = await _dbContext.Set<T>().FindAsync(id);
        return result;
    }
}