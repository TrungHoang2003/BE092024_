using DataAccess.Net.DAL;
using DataAccess.Net.DataObject;
using Microsoft.AspNetCore.Mvc;

namespace BE_092024.Controllers;


[ApiController]
[Route("api/[controller]")]
public class RoomController : Controller
{
    private readonly IRoomRepository _roomRepository;

    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    /*[HttpPost("AddRoom")]
    public IActionResult AddRoom([FromBody] Room room)
    {
       _roomRepository.addRoomToDB(room); 
        return Ok();
    }

    [HttpGet("GetRooms")]
    public IActionResult GetRooms()
    {
        return Ok(_roomRepository.getRoomsFromDB());
    }

    [HttpDelete("DeleteRoom")]
    public IActionResult DeleteRoom(int id)
    {
        _roomRepository.deleteRoomDB(id);
        return Ok();
    }

    [HttpGet("SearchRooms")]
    public IActionResult SearchRooms(int id)
    {
        return Ok(_roomRepository.searchRoom(id));
    }

    [HttpPut("UpdateRoom")]
    public IActionResult UpdateRoom(int id, [FromBody] Room room)
    {
       _roomRepository.updateRoomDB(id, room); 
       return Ok();
    }*/
}