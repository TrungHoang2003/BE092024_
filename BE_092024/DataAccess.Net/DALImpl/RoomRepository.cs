using System.Data;
using System.Data.SqlClient;
using Common.DbHelper;
using DataAccess.Net.DAL;
using DataAccess.Net.DataObject;
using DataAccess.Net.DBContext;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using SqlServerConnection = Common.DbHelper.SqlServerConnection;

namespace DataAccess.Net.DALImpl;

public class RoomRepository: IRoomRepository
{
    private BE092024_DbContext _dbContext;
    private readonly List<Room> _rooms = new List<Room>();

    public RoomRepository(BE092024_DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Room searchRoom(int id)
    {
        var conn = new SqlServerConnection().DbConnect();
        try
        {
            using var cmd = new SqlCommand($@"select * from Room where Id = '{id}'", conn);
            cmd.CommandType = CommandType.Text;
            using var reader = cmd.ExecuteReader();
            reader.Read();
            var room = new Room
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Type = reader.GetString(2)
            };
            return room;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<Room> getRoomsFromDB()
    {
        var conn = new SqlServerConnection().DbConnect();
        try
        {
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Room", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                _rooms.Add(new Room
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Type = reader["Type"].ToString()
                });
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting rooms from DB", ex);
        }
        return _rooms;
    }

    public void addRoomToDB(Room room)
    {
        if (_rooms == null)
        {
            throw new Exception("Room list is empty");
        }
        _dbContext.Room.Add(room);
        _dbContext.SaveChanges();
    }

    public void deleteRoomDB(int id)
    {
        var room = _dbContext.Room.Find(id);
        if (room == null)
            throw new Exception("Room not found");
        _dbContext.Room.Remove(room);
        _dbContext.SaveChanges();
    }

    public void updateRoomDB(int id, Room updatedRoom)
    {
        if(id != updatedRoom.Id)
            throw new Exception("Room id does not match");
        var existingRoom = _dbContext.Room.Find(id);
        if (existingRoom == null)
            throw new Exception("Room not found");
        
        existingRoom.Name = updatedRoom.Name;
        existingRoom.Type = updatedRoom.Type;
        _dbContext.SaveChanges();
    }
}