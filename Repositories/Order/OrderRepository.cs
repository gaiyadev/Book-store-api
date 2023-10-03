using System.Net;
using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.Data;
using BookstoreAPI.DTOs;
using BookstoreAPI.Pagination;
using BookstoreAPI.Repositories.CartItem;
using BookstoreAPI.Repositories.Product;
using BookstoreAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPI.Repositories.Order;

public class OrderRepository : IOrderRepository
{
    
    private readonly ApplicationDbContext _context;
    private readonly ILogger<OrderRepository> _logger;
    private readonly IProductRepository _productRepository;
    private readonly DeliveryCalculator _deliveryCalculator;
    private readonly ICartItemRepository _cartItemRepository;
    private readonly PasswordService _passwordService;

    
    public OrderRepository(
        ApplicationDbContext context,
        ILogger<OrderRepository> logger, 
        IProductRepository productRepository, 
        DeliveryCalculator deliveryCalculator,
        ICartItemRepository cartItemRepository,
        PasswordService passwordService
        )
    {
        _context = context;
        _logger = logger;
        _productRepository = productRepository;
        _deliveryCalculator = deliveryCalculator;
        _cartItemRepository = cartItemRepository;
        _passwordService = passwordService;
    }    

public async Task<Models.Order> CreateOrder(OrderCreateDto orderCreateDto, int userId)
{
    await using var transaction = await _context.Database.BeginTransactionAsync();
    try
    {
        // Calculate delivery fee and create the order
        double deliveryFee = _deliveryCalculator.CalculateDeliveryFee(
            orderCreateDto.Latitude, 
            orderCreateDto.Longitude, orderCreateDto.DeliveryMethod);
        var order = new Models.Order
        {
            UserId = userId,
            Status = "Pending",
            BillingAddress = orderCreateDto.BillingAddress,
            ShippingAddress = orderCreateDto.ShippingAddress,
            OrderDate = DateTime.UtcNow,
            PhoneNumber = orderCreateDto.PhoneNumber,
            PaymentMethod = orderCreateDto.PaymentMethod,
            DeliveryMethod = orderCreateDto.DeliveryMethod,
            DeliveryFees = deliveryFee.ToString("C2"),
            PaymentStatus = orderCreateDto.PaymentStatus,
            TrackingId = $"order_{_passwordService.GenerateRandomTrackingId(6)}",
            OrderItems = new List<Models.OrderItem>()
        };

        decimal totalAmount = 0;

        foreach (var cartItemDto in orderCreateDto.CartItems)
        {
            var product = await _productRepository.GetProduct(cartItemDto.ProductId);
            if (product == null)
            {
                await transaction.RollbackAsync();
                throw new NotFoundException($"Product with ID {cartItemDto.ProductId} not found", HttpStatusCode.NotFound);
            }

            var orderItem = new Models.OrderItem
            {
                ProductId = product.Id,
                Quantity = cartItemDto.Quantity,
                Price = product.Price,
                OrderId = order.Id
            };

            totalAmount += orderItem.Price * orderItem.Quantity;

            // Add order item to the OrderItems collection of the order object
            order.OrderItems.Add(orderItem);
        }

        order.TotalAmount = totalAmount;

        // Add the order to the context and save changes to generate the order Id
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        // Clear user's cart
        await _cartItemRepository.ClearCart(userId);

        // Commit the transaction after all operations are successful
        await transaction.CommitAsync();

        return order;
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync();
        _logger.LogError(ex.Message);
        throw new InternalServerException(ex.InnerException?.ToString() ?? ex.Message, HttpStatusCode.InternalServerError);
    }
}
    public async  Task<PagedResult<Models.Order>> GetOrders(int userId, int page, int itemsPerPage, string search)
    {
        try
        {
            var query = _context.Orders
                .Where(user => user.UserId == userId)
                .Include(u => u.OrderItems)
                .ThenInclude(product => product.Product)
                .ThenInclude(genre => genre.BookGenre)
                .Include(vendor => vendor.User)
                .OrderByDescending(order => order.Id);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = (IOrderedQueryable<Models.Order>)query.Where(order => order.BillingAddress.Contains(search));
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            var products = await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            // Create the pagination metadata
            var meta = new PaginationMetaData
            {
                TotalItems = totalItems,
                ItemCount = products.Count,
                ItemsPerPage = itemsPerPage,
                TotalPages = totalPages,
                CurrentPage = page
            };
            var paginationLinks = new PaginationLinks("http://localhost:5178/api/v1", page, totalPages, itemsPerPage);

            // Create the paged result
            return new PagedResult<Models.Order>
            {
                Data = products,
                Meta = meta,
                Links = paginationLinks
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Models.Order> GetOrder(int orderId)
    {
        var orderItem = await _context.Orders.FindAsync(orderId);
        if (orderItem == null)
        {
            throw new NotFoundException($"Order item with id {orderId} not found", HttpStatusCode.NotFound);
        }

        return orderItem;
    }
    
    public async Task<Models.Order> CancelOrderItem(int orderId)
    {
        var foundOrder = await GetOrder(orderId);
        try
        {
            foundOrder.Status = "Cancelled";
            await _context.SaveChangesAsync();
            return foundOrder;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Models.Order> DeleteOrderItem(int orderId)
    {
        var findOrder = await  GetOrder(orderId);
        _context.Orders.Remove(findOrder);
        await _context.SaveChangesAsync();
        return findOrder;
    }
}