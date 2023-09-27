﻿using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.CustomResponses.Responses;
using BookstoreAPI.DTOs;
using BookstoreAPI.Services;
using BookstoreAPI.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/products")]
[ApiVersion("1.0")]

public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly AuthUserIdExtractor _authUserIdExtractor;
    public ProductController(IProductService productService, AuthUserIdExtractor authUserIdExtractor)
    {
        _productService = productService;
        _authUserIdExtractor = authUserIdExtractor;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createProductDto)
    {
        try
        {
            var user = HttpContext.User;
            var userId = _authUserIdExtractor.GetUserId(user);
            
            var products = await _productService.CreateProduct(createProductDto, userId);
            
            var apiResponse = new List<object>
            {
                new { id = products.Id, email = products.Title }
            };
            
            return SuccessResponse.HandleCreated("Added successfully", apiResponse);
        }
        catch (BadHttpRequestException ex)
        {
            return ApplicationExceptionResponse.HandleBadRequest(ex.Message);
        } catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            var products = await _productService.GetProducts();
            return SuccessResponse.HandleOk("Fetched successfully", products, null);
        }
        catch (Exception ex)
        {
            return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
        }
    }

    [HttpGet("productId:int")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        try
        {
            var product = await _productService.GetProduct(productId);
            
            return SuccessResponse.HandleOk("Fetched successfully", product, null);
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
    
    [HttpDelete("productId:int")]
    [Authorize]
    public async Task<IActionResult> DeleteGenres(int productId)
    {
        try
        {
            await _productService.DeleteProduct(productId);
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