using BookstoreAPI.CustomExceptions.Exceptions;
using BookstoreAPI.CustomResponses.Responses;
using BookstoreAPI.DTOs;
using BookstoreAPI.Services.role;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/roles")]
[ApiVersion("1.0")]
public class RolesController : ControllerBase
{
 private readonly IRoleService _roleService;

 public RolesController(IRoleService roleService)
 {
  _roleService = roleService;
 }
 
 [HttpPost]
 public async  Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
 {
  try
  {
   var role = await _roleService.CreateRole(createRoleDto);
   var apiResponse = new List<object>
   {
    new { id = role.Id, name = role.Name }
   };
   return SuccessResponse.HandleCreated("Successfully created", apiResponse);
  }
  catch (ConflictException ex)
  {
   return ApplicationExceptionResponse.HandleConflictException(ex.Message);
  }
  catch (Exception ex)
  {
   return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);

  }
 }

 [HttpGet]
 public async Task<IActionResult> GetRoles()
 {
  try
  {
   var roles = await _roleService.GetRoles();
   return SuccessResponse.HandleOk("Fetched successfully", roles, null);
  }
  catch (Exception ex)
  {
   return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
  }
 }

 [HttpGet("roleId:int")]
 public async Task<IActionResult> GetRole(int roleId)
 {
  try
  {
   var role = await _roleService.GetRole(roleId);
   var apiResponse = new List<object>
   {
    new { id = role.Id, name = role.Name }
   };
   return SuccessResponse.HandleOk("Fetched successfully", apiResponse, null);
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
 
 
 [HttpPatch("roleId:int")]
 public async Task<IActionResult> UpdateGenres([FromBody] CreateRoleDto createRoleDto, int genreId)
 {
  try
  {
   var role = await _roleService.UpdateRole(createRoleDto, genreId);
   var apiResponse = new List<object>
   {
    new { id = role.Id, name = role.Name }
   };
   return SuccessResponse.HandleCreated("Successfully updated", apiResponse);
            
  } catch (NotFoundException ex)
  {
   return ApplicationExceptionResponse.HandleNotFound(ex.Message);
  }
  catch (Exception ex)
  {
   return ApplicationExceptionResponse.HandleInternalServerError(ex.Message);
  }
 }

 [HttpDelete("roleId:int")]
 public async Task<IActionResult> DeleteGenres(int roleId)
 {
  try
  {
   await _roleService.DeleteRole(roleId);
   return NoContent();
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