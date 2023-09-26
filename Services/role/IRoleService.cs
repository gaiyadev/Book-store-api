using BookstoreAPI.DTOs;
using BookstoreAPI.Models;

namespace BookstoreAPI.Services.role;

public interface IRoleService
{
    public Task<Role> CreateRole(CreateRoleDto createRoleDto);
    
    public Task<List<Role>> GetRoles();
    
    public Task<Role> GetRole(int roleId);
    
    public Task<Role> DeleteRole(int roleId);
    
    public Task<Role> UpdateRole(CreateRoleDto createRoleDto, int roleId);



}