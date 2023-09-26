using BookstoreAPI.DTOs;

namespace BookstoreAPI.Repositories.Role;

public interface IRoleRepository
{
    public Task<Models.Role> CreateRole(CreateRoleDto createRoleDto);
    
    public Task<List<Models.Role>> GetRoles();
    
    public Task<Models.Role> GetRole(int roleId);
    
    public Task<Models.Role> DeleteRole(int roleId);
    
    public Task<Models.Role> UpdateRole(CreateRoleDto createRoleDto, int roleId);
}