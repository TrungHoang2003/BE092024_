using System.Data;
using System.Data.SqlClient;

namespace Common.DbHelper;

public class SqlServerConnection: DbConnection<SqlConnection>
{
    SqlConnection _connection;
    public override SqlConnection DbConnect()
    {
        var connectionStr = "Server=Trung;Database=QuanLyKhachSan;Trusted_Connection=True";
        _connection = new SqlConnection(connectionStr);
       
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }
        return _connection;
    }
}