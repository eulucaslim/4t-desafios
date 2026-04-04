using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/")]
[Produces("application/json")]
public class HeathCheckController: ControllerBase
{
    [HttpGet]
    public ActionResult<Dictionary<string,string>> Get()
    {
        var heath = new Dictionary<string, string> { { "status", "OK" }, { "datetime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") } };
        return Ok(heath);
    }
}