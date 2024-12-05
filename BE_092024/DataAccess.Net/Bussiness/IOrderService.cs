using DataAccess.Net.DataObject;

namespace DataAccess.Net.Bussiness;

public interface IOrderService
{
   Task CreateOrder(Order order);
   Task UpdateOrderStatus(int orderId, string status);
   Task DeleteOrder(int orderId);
   Task<Order> GetOrderById(int orderId);
   
   Task<List<Order>> GetAllOrders();
}