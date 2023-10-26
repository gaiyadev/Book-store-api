using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.CustomResponses.Responses;
using BookstoreAPI.DTOs;
using BookstoreAPI.Repositories.Admins;
using BookstoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/admins")]
[ApiVersion("1.0")]
public class AdminController : ControllerBase
{
    private readonly IAdminRepository _adminRepository;
    private readonly JwtService _jwtService;
    private readonly AuthUserIdExtractor _authUserIdExtractor;
    
    public AdminController(
        IAdminRepository adminRepository,
        JwtService jwtService, 
        AuthUserIdExtractor authUserIdExtractor
    )
    {
        _adminRepository = adminRepository;
        _jwtService = jwtService;
        _authUserIdExtractor = authUserIdExtractor;
    }

    [HttpPost("signin")]
    public async Task<IActionResult> AdminSignIn([FromBody] SigninDto signinDto)
    {
        try
        {
            var user = await _adminRepository.AdminSignIn(signinDto);
            
            // Create a JWT token.
            var token = _jwtService.CreateToken(user.Email, user.Username, user.Id, roleId: user.RoleId);
          
            var apiResponse = new List<object>
            {
                new { id = user.Id, email = user.Email, role = new {id=user.Role.Id, name= user.Role.Name} }
            };
            return SuccessResponse.HandleOk("Successfully login", apiResponse, token);
        }
        catch (ForbiddenException ex)
        {
            return ApplicationExceptionResponse.HandleForbidden(ex.Message);
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