// using Microsoft.AspNetCore.Mvc;

// namespace vuejs_backend.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class UserInfoController : ControllerBase
// {
//     private static readonly string[] Summaries = new[]
//     {
//         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//     };

//     private readonly ILogger<UserInfoController> _logger;

//     public UserInfoController(ILogger<UserInfoController> logger)
//     {
//         _logger = logger;
//     }

//     [HttpGet(Name = "GetUserInfo")]
//     public IEnumerable<UserInfo> Get()
//     {
//         return Enumerable.Range(1, 5).Select(index => new UserInfo
//         {
//             Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             TemperatureC = Random.Shared.Next(-20, 55),
//             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//         })
//         .ToArray();
//     }
    
//     // [HttpGet(Name = "PostWeatherF")]
//     // public IEnumerable<UserInfo> Post()
//     // {
//     //     return Enumerable.Range(1, 5).Select(index => new UserInfo
//     //     {
//     //         Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//     //         TemperatureC = Random.Shared.Next(-20, 55),
//     //         Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//     //     })
//     //     .ToArray();
//     // }


// }
