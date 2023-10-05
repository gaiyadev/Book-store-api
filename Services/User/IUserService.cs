using BookstoreAPI.DTOs;

namespace BookstoreAPI.Services.User;

public interface IUserService
{
    Task<Models.User> Signup(SignupDto signupDto);
    
    Task<Models.User> SignIn(SigninDto signinDto);
    
    Task<Models.User> GetUserByEmail(string email);
    
    Task<Models.User> VerifyEmail(string token);

    Task<Models.User> ChangePassword(ChangePasswordDto changePasswordDto, int id);
    
    Task<Models.User> GetUserById(int id);
    
    Task<List<Models.User>> FindAll();
    
    Task<Models.User> DeleteUserById(int id);

    Task<Models.User> ForgotPassword(ForgotPasswordDto forgotPasswordDto);
    
    Task<Models.User> ResetPassword(ResetPasswordDto resetPasswordDto, int userId);

    Task<Models.User> VerifyResetPasswordOtp(VerifyResetPasswordOtp verifyResetPasswordOtp);
}