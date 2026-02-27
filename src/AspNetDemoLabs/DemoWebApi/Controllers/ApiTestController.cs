using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ApiTestController : ControllerBase
{

    // GET https://localhost:XXXX/api/ApiTest/
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("hello world from my API");
    }


    [HttpGet("GetAsync")]
    public async Task<IActionResult> GetAsync()
    {
        return Ok("hello world from my API");
    }

}
