using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.CustomResponses.Responses;
using BookstoreAPI.DTOs;
using BookstoreAPI.Services;
using BookstoreAPI.Services.User;
using BookstoreAPI.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion("1.0")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtService _jwtService;
    private readonly EmailService _emailService;
    private readonly EmailNotificationTemplate _emailNotificationTemplate;
    private readonly AuthUserIdExtractor _authUserIdExtractor;
    
    public UserController(
        IUserService userService,
        JwtService jwtService, 
        EmailService emailService,
        EmailNotificationTemplate emailNotificationTemplate,
        AuthUserIdExtractor authUserIdExtractor
      )
    {
        _userService = userService;
        _jwtService = jwtService;
        _emailService = emailService;
        _emailNotificationTemplate = emailNotificationTemplate;
        _authUserIdExtractor = authUserIdExtractor;
    }
    
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupDto signupDto)
    {
        try
        {
            var user = await _userService.Signup(signupDto);
            
            // Sending email
            string verificationLink = $"http://localhost:5178/verify?token={user.ResetToken}";

            _emailService.SendEmail(user.Email, "Email Verification",
                _emailNotificationTemplate.GetEmailTemplate( verificationLink, user.Otp!, signupDto.Device));

            // Response
            var apiResponse = new List<object>
            {
                new { id = user.Id, email = user.Email }
            };
            return SuccessResponse.HandleCreated("Successfully created", apiResponse);
        }
        catch (ConflictException ex)
        {
            return ApplicationExceptionResponse.HandleConflictException(ex.Message);
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
    
    [HttpPatch("verify")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string token)
    {
        try
        {
            var user = await _userService.VerifyEmail(token);
            
            var apiResponse = new List<object>
            {
                new { id = user.Id, email = user.Email }
            };
            return SuccessResponse.HandleCreated("Activated successfully", apiResponse);
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
    
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SigninDto signinDto)
    {
        try
        {
            var user = await _userService.SignIn(signinDto);
            
            // Create a JWT token.
            var token = _jwtService.CreateToken(user.Email, user.Username, user.Id, roleId: user.RoleId);
          
            var apiResponse = new List<object>
            {
                new { id = user.Id, email = user.Email }
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
    
    [HttpGet("email/{email:required}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        try
        {
            var user =  await _userService.GetUserByEmail(email);
         
            var apiResponse = new List<object>
            {
                new {Data = user }
            };
            return SuccessResponse.HandleOk("Fetched Successfully", apiResponse, null);
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
    
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var user = await _userService.GetUserById(id);

            var apiResponse = new List<object>
            {
                new { Data = user }
            };
            return SuccessResponse.HandleOk("Fetched Successfully", apiResponse, null);
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
    public async Task<IActionResult> FindAll()
    {
        try
        {
            var users = await _userService.FindAll();
            return SuccessResponse.HandleOk("Fetched successfully", users, null);
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
    
    [HttpPut("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var findUserId = HttpContext.User;
        
        var userId = _authUserIdExtractor.GetUserId(findUserId);
        
        try
        {
            var user = await _userService.ChangePassword(changePasswordDto, userId);
         
            var apiResponse = new List<object>
            {
                new { id = user.Id, email = user.Email }
            };
            
            return SuccessResponse.HandleCreated("Successfully changed password", apiResponse);
        }
        catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (ForbiddenException ex)
        {
            return ApplicationExceptionResponse.HandleForbidden(ex.Message);
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
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUserById(int id)
    {
        try
        {
            await _userService.DeleteUserById(id);
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
    
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        try
        {
            var user = await _userService.ForgotPassword(forgotPasswordDto);

            _emailService.SendEmail(user.Email, "Reset Password",
                _emailNotificationTemplate.ForgotPasswordEmailTemplate(user.Otp!));
                
            var apiResponse = new List<object>
            {
                new { id = user.Id, email = user.Email }
            };
            return SuccessResponse.HandleCreated("Successfully sent otp", apiResponse);
        }
        catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (ForbiddenException ex)
        {
            return ApplicationExceptionResponse.HandleForbidden(ex.Message);
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
    
    
    [HttpPost("verify-reset-password-otp")]
    public async Task<IActionResult> VerifyResetPasswordOtp([FromBody] VerifyResetPasswordOtp verifyResetPasswordOtp)
    {
        try
        {
            var user = await _userService.VerifyResetPasswordOtp(verifyResetPasswordOtp);
         
            var apiResponse = new List<object>
            {
                new { id = user.Id, email = user.Email }
            };
            var token = _jwtService.CreateToken(user.Email, user.Username, user.Id, roleId: user.RoleId);
            return SuccessResponse.HandleOk("Otp verified", apiResponse, token);
        }
        catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (ForbiddenException ex)
        {
            return ApplicationExceptionResponse.HandleForbidden(ex.Message);
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
    
    [HttpPut("reset-password")]
    [Authorize]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var findUserId = HttpContext.User;
        
        var userId = _authUserIdExtractor.GetUserId(findUserId);
        
        try
        {
            var user = await _userService.ResetPassword(resetPasswordDto, userId);
         
            var apiResponse = new List<object>
            {
                new { id = user.Id, email = user.Email }
            };
            
            return SuccessResponse.HandleCreated("Successfully reset password", apiResponse);
        }
        catch (NotFoundException ex)
        {
            return ApplicationExceptionResponse.HandleNotFound(ex.Message);
        }
        catch (ForbiddenException ex)
        {
            return ApplicationExceptionResponse.HandleForbidden(ex.Message);
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