using BookstoreAPI.DTOs;
using BookstoreAPI.Repositories.User;

namespace BookstoreAPI.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Models.User> Signup(SignupDto signupDto)
    {
        return await _userRepository.Signup(signupDto);
    }

    public async  Task<Models.User> SignIn(SigninDto signinDto)
    {      
        return await _userRepository.SignIn(signinDto);
    }

    public async Task<Models.User> GetUserByEmail(string email)
    {
        return await _userRepository.GetUserByEmail(email);
    }

    public async Task<Models.User> VerifyEmail(string token)
    {
        return await _userRepository.VerifyEmail(token);
    }

    public async Task<Models.User> ChangePassword(ChangePasswordDto changePasswordDto, int id)
    {
        return await _userRepository.ChangePassword(changePasswordDto, id);
    }

    public async Task<Models.User> GetUserById(int id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task<List<Models.User>> FindAll()
    {
        return await _userRepository.FindAll();
    }

    public async Task<Models.User> DeleteUserById(int id)
    {
        return await _userRepository.DeleteUserById(id);
    }
}