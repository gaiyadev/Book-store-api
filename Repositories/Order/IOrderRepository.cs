using BookstoreAPI.DTOs;
using BookstoreAPI.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.Repositories.Order;

public interface IOrderRepository
{
    Task<Models.Order> CreateOrder( OrderCreateDto orderCreateDto, int userId);
    
    Task<PagedResult<Models.Order>> GetOrders(int userId, int page, int itemsPerPage, string search);

    
    Task<Models.Order> GetOrder(int orderId);
    
    Task<Models.Order> CancelOrderItem(int orderId);
    
    Task<Models.Order> DeleteOrderItem(int orderId);
    
    Task<PagedResult<Models.Order>> FetchVendorOrderItem(int userId, int page, int itemsPerPage, string search);
    
    Task<Models.Order> ProcessOrderItem(OrderProcessingDto orderProcessingDto, int orderId);


}