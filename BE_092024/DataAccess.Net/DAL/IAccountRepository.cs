using DataAccess.Net.DataObject;
using DataAccess.Net.DTO;

namespace DataAccess.Net.DAL;

public interface IAccountRepository
{
    // Phương thức để đăng ký người dùng mới
    Task RegisterUser(User user);

    // Phương thức để tìm người dùng dựa trên tên đăng nhập
    Task<User?> User_Login(AccountDTO account);
}