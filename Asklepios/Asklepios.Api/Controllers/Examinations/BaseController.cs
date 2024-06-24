using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Examinations;

[ApiController]
[Route(BasePath + "/[controller]")]
public class BaseController : ControllerBase
{
    protected const string BasePath = "examinations-module";

    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}