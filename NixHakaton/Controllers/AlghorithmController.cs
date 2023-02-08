using Microsoft.AspNetCore.Mvc;

namespace NixHakaton.Controllers;

public class MoveModel
{
    public string GameId { get; set; }

    public string[][] Board { get; set; }

    public string Player { get; set; }
}

public class Response
{
    public int Column { get; set; }
}

public class  AlghorithmController : ControllerBase
{
    [HttpGet("/healthz")]
    public IActionResult HealthzGet()
    {
        return Ok();
    }

    [HttpPost("/move")]
    public IActionResult Move([FromBody] MoveModel model)
    {
        SolutionService solutionService = new SolutionService();
        var result = solutionService.GetNextColumnNumber(model);
        return Ok(new Response() {Column = result});
    }
}
