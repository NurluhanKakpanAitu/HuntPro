using System.Net;
using AuthServer.Common.Wrappers;
using AuthServer.Core.Application.Claim.Dto;
using AuthServer.Core.Application.Role.Dto;
using AuthServer.Core.Application.Role.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers;


public class RoleController(
    IHttpContextAccessor context, 
    IRoleCommandService roleCommandService) : BaseController(context)
{
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> CreateRoleAsync([FromBody] RoleCreateDto roleCreateDto)
    {
        return ResponseOk(await roleCommandService.CreateRoleAsync(roleCreateDto));
    }
    
    
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> UpdateRoleAsync([FromBody] RoleUpdateDto roleUpdateDto)
    {
        return ResponseOk(await roleCommandService.UpdateRoleAsync(roleUpdateDto));
    }
    
    
    [HttpDelete("{roleId}")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> DeleteRoleAsync([FromRoute] string roleId)
    {
        await roleCommandService.DeleteRoleAsync(roleId);
        return ResponseOk();
    }
}