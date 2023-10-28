using Microsoft.AspNetCore.Mvc;

namespace LoremLingerie.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}