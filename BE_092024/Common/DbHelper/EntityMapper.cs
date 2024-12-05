using System.Data.SqlClient;

namespace Common.DbHelper;

public static class EntityMapper
{
    // Phương thức ánh xạ SqlDataReader thành đối tượng T
    public static T MapReaderToEntity<T>(SqlDataReader reader) where T : class, new()
    {
        try
        {
            var entity = new T();
            var properties = typeof(T).GetProperties(); // Lấy tất cả thuộc tính lớp T

            foreach (var property in properties)
            {
                // Kiểm tra xem cột trong SqlDataReader có tồn tại hay không
                if (reader[property.Name] != DBNull.Value)
                {
                    // Gán giá trị của cột vào thuộc tính tương ứng
                    property.SetValue(entity, reader[property.Name]);
                }
            }
            return entity;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}