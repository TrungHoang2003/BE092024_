using DataAccess.Net.Bussiness;
using DataAccess.Net.DAL;
using DataAccess.Net.DataObject;
using DataAccess.Net.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_092024.Controllers;

[ApiController]
[Route("api/[controller]")]

public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }


    [HttpPost("CreateOrder")]
    public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        if (!order.OrderDetail.Any())
        {
            return BadRequest("Order must have at least one order detail");
        }

        try
        {
            order.TotalAmount = order.OrderDetail.Sum(d=>d.Subtotal);
            
            await _orderService.CreateOrder(order);
            
            return Ok(order);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("DeleteOrder")]
    public async Task<ActionResult<Order>> DeleteOrder(int id)
    {
        await _orderService.DeleteOrder(id);
        return Ok();
    }

    [HttpGet("GetOrderById")]
    public async Task<IActionResult> GetOrderById(int orderId)
    {
        var order = await _orderService.GetOrderById(orderId); 
        return Ok(order);
    }

    [HttpGet("GetOrders")]
    public async Task<IActionResult> GetOrders()
    {
        var orderList = await _orderService.GetAllOrders();
        return Ok(orderList);
    }

}