using BookstoreAPI.DTOs;
using BookstoreAPI.Models;
using BookstoreAPI.Repositories.Role;

namespace BookstoreAPI.Services.role;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    public async Task<Role> CreateRole(CreateRoleDto createRoleDto)
    {
        return await _roleRepository.CreateRole(createRoleDto);
    }

    public async Task<List<Role>> GetRoles()
    {
        return await _roleRepository.GetRoles();
    }

    public async Task<Role> GetRole(int roleId)
    {
      return  await _roleRepository.GetRole(roleId);
    }

    public async Task<Role> DeleteRole(int roleId)
    {
        return await _roleRepository.DeleteRole(roleId);
    }

    public async Task<Role> UpdateRole(CreateRoleDto createRoleDto, int roleId)
    {
        return await _roleRepository.UpdateRole(createRoleDto, roleId);
    }
}