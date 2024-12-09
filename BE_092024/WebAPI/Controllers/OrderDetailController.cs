using DataAccess.Net.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BE_092024.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderDetailController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderDetailController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetOrderDetails")]
    public async Task<IActionResult> GetOrderDetails()
    {
        var orderDetailList = await _unitOfWork.OrderDetail.GetAll();
        return Ok(orderDetailList);
    }
}