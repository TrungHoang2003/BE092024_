using DataAccess.Net.DataObject;

namespace DataAccess.Net.DAL;

public interface IRoomRepository
{
    Room searchRoom(int id);
    List<Room> getRoomsFromDB();
    void addRoomToDB(Room room);
    void updateRoomDB(int id, Room room);
    void deleteRoomDB(int id);
}