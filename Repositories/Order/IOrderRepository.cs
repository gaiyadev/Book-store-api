using BookstoreAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.Repositories.Order;

public interface IOrderRepository
{
    Task<Models.Order> CreateOrder( OrderCreateDto orderCreateDto, int userId);
    
    Task<List<Models.Order>> GetOrders();
    
    Task<Models.Order> GetOrder(int orderId);
    
    Task<Models.Order> UpdateOrder(int orderId, [FromBody] OrderUpdateDto orderUpdateDto);
    
    Task<Models.Order> DeleteOrder(int orderId);
}