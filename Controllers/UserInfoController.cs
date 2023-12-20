using Microsoft.AspNetCore.Mvc;
using vuejs_backend.Models;

namespace vuejs_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserInfoController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<UserInfoController> _logger;

    public UserInfoController(ILogger<UserInfoController> logger)
    {
        _logger = logger;
    }



}
