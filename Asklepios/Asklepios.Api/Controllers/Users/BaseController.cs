using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Users;

[ApiController]
[Route(BasePath + "/[controller]")]
public class BaseController : ControllerBase
{
    protected const string BasePath = "users-module";
}