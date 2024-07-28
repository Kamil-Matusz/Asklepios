using Asklepios.Application.Abstractions;
using Asklepios.Application.Commands;
using Asklepios.Application.Commands.Users;
using Asklepios.Application.Queries;
using Asklepios.Application.Queries.Users;
using Asklepios.Application.Security;
using Asklepios.Application.Services.Users;
using Asklepios.Core.DTO.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Users;

public class UsersController : BaseController
{
    private readonly ICommandHandler<SignUp> _signUpHandler;
    private readonly ICommandHandler<SignIn> _signInHandler;
    private readonly ICommandHandler<DeleteUserAccount> _deleteAccountHandler;
    private readonly ICommandHandler<ChangeUserRole> _changeUserRoleHandler;
    private readonly ICommandHandler<ChangeAccountStatus> _changeAccountStatus;
    private readonly ICommandHandler<GenerateUserAccount> _generateUserAccountHandler;
    private readonly IQueryHandler<GetAccountInfo, AccountDto> _getAccountInfo;
    private readonly IQueryHandler<GetAllUsers, IEnumerable<UserDto>> _getAllUsersHandler;
    private readonly ITokenStorage _tokenStorage;
    private readonly IUserService _userService;
    
    public UsersController(ICommandHandler<SignUp> signUpHandler, IQueryHandler<GetAccountInfo, AccountDto> getAccountInfo, ICommandHandler<SignIn> signInHandler, ITokenStorage tokenStorage, ICommandHandler<DeleteUserAccount> deleteAccountHandler, ICommandHandler<ChangeUserRole> changeUserRoleHandler, ICommandHandler<GenerateUserAccount> generateUserAccountHandler, IQueryHandler<GetAllUsers, IEnumerable<UserDto>> getAllUsersHandler, ICommandHandler<ChangeAccountStatus> changeAccountStatus, IUserService userService)
    {
        _signUpHandler = signUpHandler;
        _getAccountInfo = getAccountInfo;
        _signInHandler = signInHandler;
        _tokenStorage = tokenStorage;
        _deleteAccountHandler = deleteAccountHandler;
        _changeUserRoleHandler = changeUserRoleHandler;
        _generateUserAccountHandler = generateUserAccountHandler;
        _getAllUsersHandler = getAllUsersHandler;
        _changeAccountStatus = changeAccountStatus;
        _userService = userService;
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
    
    [HttpPost("signIn")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> SignIn(SignIn command)
    {
        await _signInHandler.HandlerAsync(command);
        var jwt = _tokenStorage.GetToken();
        return Ok(jwt);
    }
    
    [Authorize(Roles = "Admin, IT Employee")]
    [HttpPost("generateUserAccount")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> GenerateUserAccount(GenerateUserAccount command)
    {
        command = command with {UserId = Guid.NewGuid()};
        await _generateUserAccountHandler.HandlerAsync(command);
        
        var user = await _getAccountInfo.HandlerAsync(new GetAccountInfo() {UserId = command.UserId});
        return Ok(user);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
    
    [Authorize]
    [HttpGet("myAccount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AccountDto>> AccountInfo()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }
        
        var userId = Guid.Parse(User.Identity?.Name);
        
        var user = await _getAccountInfo.HandlerAsync(new GetAccountInfo() {UserId = userId});
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteUserAccount(Guid userId)
    {
        await _deleteAccountHandler.HandlerAsync(new DeleteUserAccount(userId));
        return NoContent();
    }

    
    [Authorize(Roles = "Admin")]
    [HttpPut("{userId:guid}/changeUserRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ChangeUserRole(Guid userId, ChangeUserRole command)
    {
        await _changeUserRoleHandler.HandlerAsync(command with { UserId = userId, Role  = command.Role });
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{userId:guid}/changeAccountStatus")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ChangeAccountStatus(Guid userId, ChangeAccountStatus command)
    {
        await _changeAccountStatus.HandlerAsync(command with { UserId = userId, status  = command.status });
        return Ok();
    }
    
    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Logout()
    {
        _tokenStorage.ClearToken();
        
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers([FromQuery] GetAllUsers query)
        => Ok(await _getAllUsersHandler.HandlerAsync(query));
    
    [Authorize]
    [HttpGet("nursesList")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<UserAutocompleteDto>>> GetNursesList()
    {
        var users = await _userService.GetNursesList();
        return Ok(users);
    }
    
    [Authorize]
    [HttpGet("doctorsList")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<UserAutocompleteDto>>> GetDoctorsList()
    {
        var users = await _userService.GetDoctorsList();
        return Ok(users);
    }
}