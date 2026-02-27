namespace DemoWebApiDB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestDemoController : ControllerBase
{

    // GET /api/TestDemo
    // GET /api/TestDemo/Index
    [HttpGet]
    public IActionResult Index()
    {
        // return new OkResult();

        // return new OkObjectResult("Microsoft");

        return Ok("Microsoft");
    }


    [HttpGet]
    public async Task<IActionResult> IndexAsync()
    {
        var message = await Task<string>.Run(() => "Microsoft");
        return Ok(message);
    }

}
