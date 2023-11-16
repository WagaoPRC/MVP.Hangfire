using Hangfire;
using Microsoft.AspNetCore.Mvc;
using MyCronJob;

namespace WebApiJob.Controller;

[Controller]
public class HomeController : ControllerBase
{
    public HomeController() 
    {

    }

    [HttpGet("myjob")]
    public IActionResult actionResult(DateTime date, TimeSpan valueTime)
    {
        BackgroundJob.Schedule<Worker>(x => x.MyDelayedjob(), valueTime);
        return Ok();
    }
}
