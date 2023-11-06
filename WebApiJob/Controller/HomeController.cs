using Microsoft.AspNetCore.Mvc;

namespace WebApiJob.Controller;

[Controller]
public class HomeController : ControllerBase
{
    public HomeController() 
    {

    }

    [HttpGet]
    public IActionResult actionResult()
    {
        return Ok();
    }
}
