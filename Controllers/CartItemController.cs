using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.CustomResponses.Responses;
using BookstoreAPI.DTOs;
using BookstoreAPI.Repositories.CartItem;
using BookstoreAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/CartItems")]
[ApiVersion("1.0")]
public class CartItemController : ControllerBase
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly AuthUserIdExtractor _authUserIdExtractor;
    public CartItemController(ICartItemRepository cartItemRepository, AuthUserIdExtractor authUserIdExtractor)
    {
        _cartItemRepository = cartItemRepository;
        _authUserIdExtractor = authUserIdExtractor;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddToCart([FromBody] CartItemDto cartItemDto)
    {
        try
        {
            var user = HttpContext.User;
            var userId = _authUserIdExtractor.GetUserId(user);
            var cart =  await _cartItemRepository.AddToCart(cartItemDto, userId);
            var apiResponse = new List<object>
            {
                new { id = cart.Id, productId = cart.ProductId }
            };
            return SuccessResponse.HandleCreated("Added successfully", apiResponse);
        }
        catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCartItems()
    {
        try
        {
            var user = HttpContext.User;
            var userId = _authUserIdExtractor.GetUserId(user);

            var cartItems = await _cartItemRepository.GetCartItemsForUser(userId);
            return SuccessResponse.HandleOk("Added successfully", cartItems, null);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateCartItem(int id, [FromBody] UpdateCartItemDto updateCartItemDto)
    {
        try
        {
           var cart =  await _cartItemRepository.UpdateCartItemQuantity(id, updateCartItemDto.Quantity);
            var apiResponse = new List<object>
            {
                new { id = cart.Id, productId = cart.ProductId }
            };
            return SuccessResponse.HandleCreated("Added successfully", apiResponse);
        }
        catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCartItem(int id)
    {
        try
        {
            await _cartItemRepository.DeleteCartItem(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }
}