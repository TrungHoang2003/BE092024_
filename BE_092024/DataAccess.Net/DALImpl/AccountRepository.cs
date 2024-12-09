using Common.DbHelper;
using DataAccess.Net.DAL;
using DataAccess.Net.DataObject;
using DataAccess.Net.DBContext;
using DataAccess.Net.DTO;
using DataAccess.Net.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace DataAccess.Net.DALImpl;

public class AccountRepository: IAccountRepository
{
    private readonly BE092024_DbContext _context;

    public AccountRepository(BE092024_DbContext context)
    {
        _context = context;
    }

    public async Task RegisterUser(User user)
    {
        await _context.User.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> User_Login(AccountDTO account)
    {
        return await _context.User
            .FirstOrDefaultAsync(u => u.UserName == account.UserName);
    }
}