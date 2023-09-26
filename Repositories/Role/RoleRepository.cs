using System.Net;
using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.Data;
using BookstoreAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPI.Repositories.Role;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RoleRepository> _logger;
    
    public RoleRepository(ApplicationDbContext context, ILogger<RoleRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Models.Role> CreateRole(CreateRoleDto createRoleDto)
    {
        var findRole = await _context.Roles.FirstOrDefaultAsync(role => role.Name == createRoleDto.Name);

        if (findRole != null)
        {
            throw new ConflictException("Role already exists", HttpStatusCode.Conflict);
        }
        try
        {
            var role = new Models.Role()
            {
                Name = createRoleDto.Name
            };
            await _context.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<List<Models.Role>> GetRoles()
    {
        try
        {
            return await _context.Roles
                .OrderByDescending(role => role.Id)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Models.Role> GetRole(int roleId)
    {
        var findRole = await _context.Roles.FindAsync(roleId);
        
        if (findRole == null)
        {
            throw new NotFoundException($"Genre with {roleId} not found", HttpStatusCode.NotFound);
        }

        return findRole;
    }

    public async Task<Models.Role> DeleteRole(int roleId)
    {
        var findRole = await GetRole(roleId);
        if (findRole == null)
        {
            throw new NotFoundException($"Genre with {roleId} not found", HttpStatusCode.NotFound);
        }
        _context.Roles.Remove(findRole);
        await _context.SaveChangesAsync();
        return findRole;
    }

    public async Task<Models.Role> UpdateRole(CreateRoleDto createRoleDto, int roleId)
    {
        var findRole = await GetRole(roleId);
        if (findRole == null)
        {
            throw new NotFoundException($"Genre with {roleId} not found", HttpStatusCode.NotFound);
        }
        findRole.Name = createRoleDto.Name;
        await _context.SaveChangesAsync();
        return findRole;
    }
}