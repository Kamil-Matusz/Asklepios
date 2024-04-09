using Asklepios.Application.Abstractions;
using Asklepios.Application.Commands;
using Asklepios.Application.Queries;
using Asklepios.Core.DTO.Users;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Users;

public class UsersController : BaseController
{
    private readonly ICommandHandler<SignUp> _signUpHandler;
    private readonly IQueryHandler<GetAccountInfo, AccountDto> _getAccountInfo;
    
    public UsersController(ICommandHandler<SignUp> signUpHandler, IQueryHandler<GetAccountInfo, AccountDto> getAccountInfo)
    {
        _signUpHandler = signUpHandler;
        _getAccountInfo = getAccountInfo;
    }
    
    [HttpPost("signUp")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUp(SignUp command)
    {
        command = command with {UserId = Guid.NewGuid()};
        await _signUpHandler.HandlerAsync(command);
        
        var user = await _getAccountInfo.HandlerAsync(new GetAccountInfo() {UserId = command.UserId});
        return Ok(user);
    }
    
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AccountDto>> GetUser(Guid userId)
    {
        var user = await _getAccountInfo.HandlerAsync(new GetAccountInfo() {UserId = userId});
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}