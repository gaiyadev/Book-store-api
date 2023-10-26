using System.Net;
using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.Data;
using BookstoreAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPI.Repositories.Admins;

public class AdminRepository : IAdminRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AdminRepository> _logger;

    public AdminRepository(ApplicationDbContext context,  ILogger<AdminRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Models.Admin> AdminSignIn(SigninDto signinDto)
    {
        var findEmail = await _context.Admins.Where(admin => admin.Email == signinDto.LoginId)
            .Include(role => role.Role)
            .FirstOrDefaultAsync();

        if (findEmail == null)
        {
            throw new ForbiddenException("Invalid username or password.", HttpStatusCode.Forbidden);
        }
        if (findEmail.Password != "password")
        {
            throw new ForbiddenException("Invalid username or password.", HttpStatusCode.Forbidden);
        }

        return findEmail;
    }
}