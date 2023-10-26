using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.CustomResponses.Responses;
using BookstoreAPI.DTOs;
using BookstoreAPI.Repositories.Order;
using BookstoreAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/orders")]
[ApiVersion("1.0")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly AuthUserIdExtractor _authUserIdExtractor;

    public OrderController(IOrderRepository orderRepository,  AuthUserIdExtractor authUserIdExtractor)
    {
        _orderRepository = orderRepository;
        _authUserIdExtractor = authUserIdExtractor;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
    {
        try
        {
            var user = HttpContext.User;
            var userId = _authUserIdExtractor.GetUserId(user);

            var createdOrder = await _orderRepository.CreateOrder(orderCreateDto, userId);
            var apiResponse = new List<object>
            {
                new { id = createdOrder.Id, productId = createdOrder.UserId }
            };
            return SuccessResponse.HandleCreated("Ordered successfully", apiResponse);
        }
        catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (InternalServerException ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetOrders([FromQuery] string search = "", int page = 1, int itemsPerPage=10)
    {
        try
        {
            var user = HttpContext.User;
            var userId = _authUserIdExtractor.GetUserId(user);
            var orderItem = await _orderRepository.GetOrders(userId, page, itemsPerPage, search);
            return SuccessResponse.HandleOk("Fetched successfully", orderItem,  null);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }

    [HttpGet("{orderId:int}")]
    [Authorize]
    public async Task<IActionResult> GetOrder(int orderId)
    {
        try
        {
            var orderItem = await _orderRepository.GetOrder(orderId);
            return SuccessResponse.HandleOk("Ordered successfully", orderItem,  null);
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
    
    [HttpPatch("{orderId:int}/cancel")]
    [Authorize]
    public async Task<IActionResult> CancelOrderItem(int orderId)
    {
        try
        {
            var orderItem = await _orderRepository.CancelOrderItem(orderId);
            return SuccessResponse.HandleOk("Order cancel successfully", orderItem,  null);
        }
        catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (InternalServerException ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }
    
    [HttpDelete("{orderId:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteOrderItem(int orderId)
    {
        try
        {
            await _orderRepository.DeleteOrderItem(orderId);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (InternalServerException ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }
    
    [HttpGet("vendor")]
    [Authorize]
    public async Task<IActionResult> FetchVendorOrderItem([FromQuery] string search = "", int page = 1, int itemsPerPage=10)
    {
        try
        {
            var user = HttpContext.User;
            var userId = _authUserIdExtractor.GetUserId(user);
            var orderItems = await _orderRepository.FetchVendorOrderItem(userId, page, itemsPerPage, search);
            return SuccessResponse.HandleOk("Fetched successfully", orderItems,  null);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }


    [HttpPatch("{orderId:int}/process")]
    [Authorize]
    public async Task<IActionResult> ProcessOrderItem([FromBody] OrderProcessingDto orderProcessingDto ,int orderId)
    {
        try
        {
          var orderItem=  await _orderRepository.ProcessOrderItem(orderProcessingDto, orderId);
          var apiResponse = new List<object>
          {
              new { id = orderItem.Id, productId = orderItem.UserId , status= orderItem.Status}
          };
          return SuccessResponse.HandleCreated("Ordered processed successfully", apiResponse);
        }
        catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (InternalServerException ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }
}
