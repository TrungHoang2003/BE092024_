using DataAccess.Net.DAL;
using DataAccess.Net.DataObject;
using Microsoft.AspNetCore.Mvc;

namespace BE_092024.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("GetProductList")]
    public IActionResult getProductList()
    {
        var list = _productRepository.getProductList();
        return Ok(list);
    }

    [HttpPost("AddProduct")]
    public IActionResult addProduct([FromBody] Product product)
    {
        _productRepository.addProduct(product);
        return Ok();
    }

    [HttpGet("SearchProduct")]
    public IActionResult searchProduct(string productName, string category)
    {
        var product = _productRepository.searchProduct(productName, category);
        return Ok(product);
    }
    
    [HttpDelete("DeleteProduct")]
    public IActionResult deleteProduct(int id)
    {
        _productRepository.deleteProduct(id);
        return Ok();
    }
    
    [HttpGet("SortProduct")]
    public IActionResult sortProduct(string sortType)
    {
        var product = _productRepository.sortProduct(sortType);
        return Ok(product);
    }

    [HttpPost("ExportProductToExcel")]
    public IActionResult exportProductToExcel()
    {
        _productRepository.exportProductToExcel();
        return Ok();
    }

    [HttpPost("ImportProductFromExcel")]
    public IActionResult importProductFromExcel(int id)
    {
        _productRepository.importProductFromExcel(id);
        return Ok();
    }
}