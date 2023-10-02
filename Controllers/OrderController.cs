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

    // POST: api/Order
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

    // GET: api/Order/{orderId}
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(int orderId)
    {
        try
        {
            var order = await _orderRepository.GetOrder(orderId);
            return SuccessResponse.HandleOk("Ordered successfully", order,  null);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }

    // PUT: api/Order/{orderId}
    [HttpPut("{orderId}")]
    public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] OrderUpdateDto orderUpdateDto)
    {
        try
        {
            await _orderRepository.UpdateOrder(orderId, orderUpdateDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);

        }
    }

    // DELETE: api/Order/{orderId}
    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        try
        {

            await _orderRepository.DeleteOrder(orderId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }
}
