using System.Net;
using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.Data;
using BookstoreAPI.DTOs;
using BookstoreAPI.Repositories.Product;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPI.Repositories.CartItem;

public class CartItemRepository : ICartItemRepository
{
    private readonly ILogger<CartItemRepository> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IProductRepository _productRepository;

    public CartItemRepository(ILogger<CartItemRepository> logger, ApplicationDbContext context, IProductRepository productRepository)
    {
        _logger = logger;
        _context = context;
        _productRepository = productRepository;
    }
    
    public async Task<Models.CartItem> AddToCart(CartItemDto cartItemDto, int userId)
    {
        try
        {
            var product = await _productRepository.GetProduct(cartItemDto.ProductId);

            if (product == null)
            {
                throw new NotFoundException($"Product with ID {cartItemDto.ProductId} not found", HttpStatusCode.NotFound);
            }

            Models.CartItem addToCart;

            // Check if the product is already in the user's cart
            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(item => item.ProductId == cartItemDto.ProductId && item.UserId == userId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cartItemDto.Quantity; // If yes, update the quantity
                addToCart = existingCartItem;
            }
            else
            {
                addToCart = new Models.CartItem
                {
                    Quantity = cartItemDto.Quantity,
                    UserId = userId,
                    ProductId = cartItemDto.ProductId,
                    Price = product.Price
                };

                _context.CartItems.Add(addToCart);
            }
            // Save changes once after all modifications are done
            await _context.SaveChangesAsync();
            return addToCart;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<List<Models.CartItem>> GetCartItemsForUser(int userId)
    {
        try
        {
            return await _context.CartItems
                .Where(item => item.UserId == userId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Models.CartItem> UpdateCartItemQuantity(int cartItemId, int newQuantity)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);

        if (cartItem != null)
        {
            cartItem.Quantity = newQuantity;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new NotFoundException($"Cart item with ID {cartItemId} not found", HttpStatusCode.NotFound);
        }
        return cartItem;
    }

    public async Task<Models.CartItem> DeleteCartItem(int cartItemId)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);

        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new NotFoundException($"Cart item with ID {cartItemId} not found", HttpStatusCode.NotFound);
        }

        return cartItem;
    }

    public async Task<List<Models.CartItem>> ClearCart(int userId)
    {
        try
        {
            var cartItems = await _context.CartItems
                .Where(item => item.UserId == userId)
                .ToListAsync();

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            return cartItems;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

}