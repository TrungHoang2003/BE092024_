using DataAccess.Net.DAL;
using DataAccess.Net.DataObject;
using DataAccess.Net.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BE_092024.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetProducts")]
    public async Task<IActionResult> GetProducts()
    { 
        var productList = await _unitOfWork.Products.GetAll();
        return Ok(productList);
    }

    [HttpGet("SearchProducts{id}")]
    public async Task<IActionResult> GetProducts(int id)
    {
        var productList = await _unitOfWork.Products.Search(id);
        return Ok(productList);
    }

    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddProduct([FromBody] Product newProduct)
    { 
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        await _unitOfWork.Products.Insert(newProduct);
        return Ok();
    }

    [HttpPut("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        
        await _unitOfWork.Products.Update(product);
        return Ok();
    }

    [HttpDelete("DeleteProduct")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = _unitOfWork.Products.Search(id);
        await _unitOfWork.Products.Delete(await product);
        return Ok();
    }
}