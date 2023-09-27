using System.Net;
using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.Data;
using BookstoreAPI.DTOs;
using BookstoreAPI.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BookstoreAPI.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserRepository> _logger;
    private readonly PasswordService _passwordService;

    public UserRepository(ApplicationDbContext context,  ILogger<UserRepository> logger,
        PasswordService passwordService)
    {
        _context = context;
        _logger = logger;
        _passwordService = passwordService;
    }
    public async Task<Models.User> Signup(SignupDto signupDto)
    {
        var findEmail = await _context.Users.AnyAsync(user => user.Email == signupDto.Email);

        if (findEmail)
        {
            throw new ConflictException("Email address already taken", HttpStatusCode.Conflict);
        }
        var findUsername = await _context.Users.AnyAsync(user => user.Username == signupDto.Username);

        if (findUsername)
        {
            throw new ConflictException("Username already taken", HttpStatusCode.Conflict);
        }
        
        try
        {
            string hashedPassword = _passwordService.HashPassword(signupDto.Password);
            string verificationToken = Guid.NewGuid().ToString("N").Substring(0, 8) + DateTime.Now.Ticks.ToString("X");
            string otp = _passwordService.GenerateOtp();
            var user = new Models.User()
            {
                Email = signupDto.Email,
                Password = hashedPassword,
                Username = signupDto.Username,
                RoleId = signupDto.RoleId,
                ResetToken = verificationToken,
                Otp = otp
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (DbUpdateException dbEx) when (dbEx.InnerException is PostgresException pgEx)
        {
            _logger.LogError(pgEx.Message);
            throw new InternalServerException(pgEx.Message, HttpStatusCode.InternalServerError);
        
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Models.User> SignIn(SigninDto signinDto)
    {
        var user = await GetUserByEmailOrUsername(signinDto.LoginId);
        if (user == null)
        {
            throw new ForbiddenException("Invalid username or password.", HttpStatusCode.Forbidden);
        }

        if (!_passwordService.VerifyPassword(signinDto.Password, user.Password))
        {
            throw new ForbiddenException("Invalid username or password.", HttpStatusCode.Forbidden);
        }

        if (user.IsActive == false)
        {
            throw new ForbiddenException("Account is not active", HttpStatusCode.Forbidden);
        }
        return user;
    }

    public async Task<Models.User> GetUserByEmail(string email)
    {
        var findUser = await _context.Users.Where(u => u.Email == email)
            .FirstOrDefaultAsync();

        if (findUser == null)
        {
            throw new NotFoundException("User not found", HttpStatusCode.NotFound);
        }

        return findUser;
    }

    public async Task<Models.User> VerifyEmail(string token)
    {
        var findUser = await _context.Users.Where(u => u.ResetToken == token || u.Otp == token).FirstOrDefaultAsync();
        
        if (findUser == null)
        {
            throw new NotFoundException("Token invalid or expired", HttpStatusCode.NotFound);
        }
        try
        {
            findUser.ResetToken = null;
            findUser.Otp = null;
            findUser.IsActive = true;
            await _context.SaveChangesAsync();
            return findUser;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Models.User> ChangePassword(ChangePasswordDto changePasswordDto, int id)
    {
        var findUser = await GetUserById(id);

        if (findUser == null)
        {
            throw new NotFoundException("User not found", HttpStatusCode.NotFound);
        }

        if (!_passwordService.VerifyPassword(changePasswordDto.Password, findUser.Password))
        {
            throw new ForbiddenException("Current password not correct", HttpStatusCode.Forbidden);
        }

        try
        {
            string hashedPassword = _passwordService.HashPassword(changePasswordDto.NewPassword);
            findUser.Password = hashedPassword;
            await _context.SaveChangesAsync();
            return findUser;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Models.User> GetUserById(int id)
    {
        var findUser = await _context.Users
            .Include(u=> u.Role).
            FirstOrDefaultAsync(user => user.Id == id);

        if (findUser == null)
        {
            throw new NotFoundException("User not found", HttpStatusCode.NotFound);
        }

        return findUser;
    }

    public async Task<List<Models.User>> FindAll()
    {
        try
        {
           return await _context.Users
               .Include(u=> u.Role)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Models.User> DeleteUserById(int id)
    {
        var findUser = await GetUserById(id);
        if (findUser == null)
        {
            throw new NotFoundException("User Not found", HttpStatusCode.NotFound);
        }
        try
        {
            _context.Users.Remove(findUser);
            await _context.SaveChangesAsync();
            return findUser;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Models.User> GetUserByEmailOrUsername(string loginId)
    {
        var getUserByEmailOrUsername =  await _context.Users
            .Where(u => u.Email == loginId || u.Username == loginId)
            .FirstOrDefaultAsync();
        
        if (getUserByEmailOrUsername == null)
        {
            throw new NotFoundException("User not found", HttpStatusCode.NotFound);
        }
        return getUserByEmailOrUsername;
    }
}