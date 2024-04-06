using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Departments;

[ApiController]
[Route(BasePath + "/[controller]")]
public class BaseController : ControllerBase
{
    protected const string BasePath = "departments-module";

    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}