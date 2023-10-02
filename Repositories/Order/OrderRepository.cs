using System.Net;
using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.Data;
using BookstoreAPI.DTOs;
using BookstoreAPI.Enums;
using BookstoreAPI.Models;
using BookstoreAPI.Repositories.Product;

namespace BookstoreAPI.Repositories.Order;

public class OrderRepository : IOrderRepository
{
    
    private readonly ApplicationDbContext _context;
    private readonly ILogger<OrderRepository> _logger;
    private readonly IProductRepository _productRepository;
    
    public OrderRepository(ApplicationDbContext context,
        ILogger<OrderRepository> logger, IProductRepository productRepository)
    {
        _context = context;
        _logger = logger;
        _productRepository = productRepository;
    }    
    public async Task<Models.Order> CreateOrder(OrderCreateDto orderCreateDto, int userId)
    {
        try
        {
            var order = new Models.Order
            {
                UserId = userId,
                Status = OrderStatus.Pending,
                BillingAddress = orderCreateDto.BillingAddress,
                ShippingAddress = orderCreateDto.ShippingAddress,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            decimal totalAmount = 0;

            foreach (var cartItemDto in orderCreateDto.CartItems)
            {
                var product = await _productRepository.GetProduct(cartItemDto.ProductId);
                if (product == null)
                {
                    throw new NotFoundException($"Product with ID {cartItemDto.ProductId} not found", HttpStatusCode.NotFound);
                }

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = cartItemDto.Quantity,
                    Price = product.Price,
                    PaymentStatus = PaymentStatus.Paid
                };

                totalAmount += orderItem.Price * orderItem.Quantity;
                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = totalAmount;

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }


    public Task<List<Models.Order>> GetOrders()
    {
        throw new NotImplementedException();
    }

    public Task<Models.Order> GetOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Order> UpdateOrder(int orderId, OrderUpdateDto orderUpdateDto)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Order> DeleteOrder(int orderId)
    {
        throw new NotImplementedException();
    }
}