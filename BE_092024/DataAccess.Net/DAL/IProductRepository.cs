using DataAccess.Net.DataObject;

namespace DataAccess.Net.DAL;

public interface IProductRepository
{
    void addProduct(Product product);
    void importProductFromExcel(int id);
    void exportProductToExcel();
    List<Product> getProductList();
    IEnumerable<Product> searchProduct(string productName, string category);
    void deleteProduct(int id);
    IEnumerable<Product> sortProduct(string sortType);
}