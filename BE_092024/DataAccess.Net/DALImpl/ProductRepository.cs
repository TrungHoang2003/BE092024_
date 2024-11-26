using System.Collections;
using DataAccess.Net.DAL;
using DataAccess.Net.DataObject;
using OfficeOpenXml;
using OfficeOpenXml;
using System.Collections.Generic;


namespace DataAccess.Net.DALImpl;

public class ProductRepository:IProductRepository
{
    private readonly List<Product> _products = new List<Product>();

    public void addProduct(Product product)
    {
        _products.Add(product);
    }

    public void importProductFromExcel(int id)
    {
        string filePath = @"C:\\Users\\trung\\OneDrive\\Desktop\\BE092024.xlsx";
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets["Product"];
        
        if (worksheet == null)
            throw new Exception("File khong ton tai");

        var endRow = worksheet.Dimension.End.Row;
        
        for (int row = 2; row <= endRow; row++)
        {
            if (!int.TryParse(worksheet.Cells[row, 1].Text, out var parseId) || parseId != id) continue;
            var product = new Product
            {
                Id = parseId,
                Name = worksheet.Cells[row, 2].Text,
                Price = int.TryParse(worksheet.Cells[row, 3].Text, out var parsePrice) ? parsePrice : 0,
                Category = worksheet.Cells[row, 4].Text
            };
            _products.Add(product);
        }
    }

    public void exportProductToExcel()
    {
        string filePath = @"C:\\Users\\trung\\OneDrive\\Desktop\\BE092024.xlsx";
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var file = new FileInfo(filePath);
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets["Product"];
        
        if (worksheet != null)
           package.Workbook.Worksheets.Delete("Product"); 
        
        worksheet = package.Workbook.Worksheets.Add("Product");
        worksheet.Cells[1,1].Value = "Id";
        worksheet.Cells[1,2].Value = "Name";
        worksheet.Cells[1,3].Value = "Price";
        worksheet.Cells[1,4].Value = "Category";

        var row = 2;
        foreach (var product in _products)
        {
            worksheet.Cells[row,1].Value = product.Id;
            worksheet.Cells[row,2].Value = product.Name;
            worksheet.Cells[row,3].Value = product.Price;
            worksheet.Cells[row,4].Value = product.Category;
            row++;
        }

        package.SaveAs(file);
    }

    public List<Product> getProductList()
    {
        return _products;
    }

    public IEnumerable<Product> searchProduct(string productName, string category)
    {
        return _products.Where(p => p.Name != null
                                    && p.Name.Contains(productName)
                                    && p.Category != null
                                    && p.Category.Contains(category));
    }

    public void deleteProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null) _products.Remove(product);
    }

    public IEnumerable<Product> sortProduct(string sortType)
    {
        switch (sortType)
        {
          case "price_increase":
              return _products.OrderBy(p => p.Price);
          case "price_decrease":
              return _products.OrderByDescending(p => p.Price);
          case "name_AZ":
              return _products.OrderBy(p => p.Name);
          case "name_ZA":
              return _products.OrderByDescending(p => p.Name);
          default:
              return _products;
        } 
    }
}