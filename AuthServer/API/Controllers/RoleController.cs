using System.Net;
using AuthServer.Common.Wrappers;
using AuthServer.Core.Application.Claim.Dto;
using AuthServer.Core.Application.Claim.Services;
using AuthServer.Core.Application.Role.Dto;
using AuthServer.Core.Application.Role.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers;


public class RoleController(
    IHttpContextAccessor context, 
    IRoleCommandService roleCommandService,
    IClaimCommandService claimCommandService,
    IClaimQueryService claimQueryService,
    IRoleQueryService roleQueryService
    ) : BaseController(context)
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
    
    [HttpPost("add-claim")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> AddClaimAsync([FromBody] ClaimCreateDto claimCreateDto)
    {
        return ResponseOk(await claimCommandService.CreateAsync(claimCreateDto));
    }
    
    [HttpDelete("remove-claim")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> RemoveClaimAsync([FromBody] RemoveClaimDto removeClaimDto)
    {
        await claimCommandService.DeleteAsync(removeClaimDto);
        return ResponseOk();
    }
    
    [HttpGet("{roleId}/claims/paged")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> GetRoleClaimsAsync(
        [FromRoute] string roleId,
        [FromQuery] int pageNum = 1,
        [FromQuery] int pageSize = 20)
    {
        return ResponseOk(await claimQueryService.GetClaimsAsync(roleId, pageNum, pageSize));
    }
    
    [HttpGet("{roleId}/claims/all")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> GetAllRoleClaimsAsync([FromRoute] string roleId)
    {
        return ResponseOk(await claimQueryService.GetAllClaimsAsync(roleId));
    }
    
    [HttpGet("paged")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> GetRolesAsync(
        [FromQuery] int pageNum = 1,
        [FromQuery] int pageSize = 20)
    {
        return ResponseOk(await roleQueryService.GetRolesAsync(pageNum, pageSize));
    }
    
    [HttpGet("all")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> GetAllRolesAsync()
    {
        return ResponseOk(await roleQueryService.GetAllRolesAsync());
    }
    
    [HttpGet("{roleId}")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> GetRoleByIdAsync([FromRoute] string roleId)
    {
        return ResponseOk(await roleQueryService.GetRoleByIdAsync(roleId));
    }
    
    [HttpPut("update-claims")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> UpdateRoleClaimsAsync([FromBody] UpdateRoleClaimsDto updateRoleClaimsDto)
    {
        var res = await roleCommandService.UpdateRoleClaimsAsync(updateRoleClaimsDto);
        return ResponseOk(res);
    }
}