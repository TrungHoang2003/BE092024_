using DataAccess.Net.DataObject;
using DataAccess.Net.UnitOfWork;

namespace DataAccess.Net.Bussiness;

public class OrderService: IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateOrder(Order order)
    {
        foreach (var orderDetail in order.OrderDetail)
        {
            var product = await _unitOfWork.Products.Search(orderDetail.ProducId);
            if(product == null)
                throw new Exception("Product not found");
            
            if(product.Stock < orderDetail.Quantity)
                throw new Exception("Product out of stock");
            
            product.Stock -= orderDetail.Quantity; //Cập nhật tồn kho
            await _unitOfWork.Products.Update(product); //Cập nhật sản phẩm 
        }
        
        //Thêm đơn hàng vào databse
        await _unitOfWork.Orders.Insert(order);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateOrderStatus(int orderId, string status)
    {
        var order = await _unitOfWork.Orders.Search(orderId);
        if(order == null)
            throw new Exception("Order not found");
        
        order.Status = status;
        await _unitOfWork.Orders.Update(order);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteOrder(int orderId)
    {
       var order = _unitOfWork.Orders.Search(orderId);
       if(order == null)
           throw new Exception("Order not found");
       await _unitOfWork.Orders.Delete(await order);
       await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Order> GetOrderById(int orderId)
    {
        return await  _unitOfWork.Orders.Search(orderId);
    }

    public async Task<List<Order>> GetAllOrders()
    {
        var orders = await _unitOfWork.Orders.GetAll();
        return orders;
    }
}