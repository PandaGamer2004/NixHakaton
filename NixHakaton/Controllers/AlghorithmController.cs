using Microsoft.AspNetCore.Mvc;

namespace NixHakaton.Controllers;

public class AlghorithmController : ControllerBase
{
    [HttpPost("/move")]
    public IActionResult Move(MoveModel model)
    {
        return Ok();
    }
}


public class MoveModel
{
    public int GameId { get; set; }

    public string[][] Board { get; set; }

    public string Player { get; set; }
}