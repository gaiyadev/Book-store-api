using BookstoreAPI.DTOs;

namespace BookstoreAPI.Repositories.CartItem;

public interface ICartItemRepository
{
    Task<Models.CartItem> AddToCart( CartItemDto cartItemDto, int userId);
    
    Task<List<Models.CartItem>> GetCartItemsForUser(int userId);

    Task<Models.CartItem> UpdateCartItemQuantity(int cartItemId, int newQuantity);

    Task<Models.CartItem> DeleteCartItem(int cartItemId);

}