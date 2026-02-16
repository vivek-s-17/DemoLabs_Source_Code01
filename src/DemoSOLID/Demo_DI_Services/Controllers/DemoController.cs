using Demo_DI_Services.Services;

using Microsoft.AspNetCore.Mvc;


namespace Demo_DI_Services.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DemoController : ControllerBase
{

    private readonly IMySingletonService _objSingletonService1;
    private readonly IMySingletonService _objSingletonService2;

    private readonly IMyScopedService _objScopedService1;
    private readonly IMyScopedService _objScopedService2;

    private readonly IMyTransientService _objTransientService1;
    private readonly IMyTransientService _objTransientService2;

    public DemoController(
        IMySingletonService objSingletonService1,
        IMySingletonService objSingletonService2,
        IMyScopedService objScopedService1,
        IMyScopedService objScopedService2,
        IMyTransientService objTransientService1,
        IMyTransientService objTransientService2)
    {
        _objSingletonService1 = objSingletonService1;
        _objSingletonService2 = objSingletonService2;

        _objScopedService1 = objScopedService1;
        _objScopedService2 = objScopedService2;

        _objTransientService1 = objTransientService1;
        _objTransientService2 = objTransientService2;
    }


    // [Route("/api/Demo/Attempt1")]
    // [HttpGet]
    [HttpGet("Attempt1")]
    public IActionResult Attempt1()
    {
        var retVal = new[]
        {
            new
            {
                InstanceId = 1,
                SingletonId = _objSingletonService1.Id,
                ScopedId = _objScopedService1.Id,
                TransientId = _objTransientService1.Id
            },
            new
            {
                InstanceId = 2,
                SingletonId = _objSingletonService2.Id,
                ScopedId = _objScopedService2.Id,
                TransientId = _objTransientService2.Id
            }
        };

        return Ok(retVal);
    }


    [HttpGet("Attempt2")]
    public IActionResult Attempt2()
    {
        var retVal = new[]
        {
            new
            {
                InstanceId = 1,
                SingletonId = _objSingletonService1.Id,
                ScopedId = _objScopedService1.Id,
                TransientId = _objTransientService1.Id
            },
            new
            {
                InstanceId = 2,
                SingletonId = _objSingletonService2.Id,
                ScopedId = _objScopedService2.Id,
                TransientId = _objTransientService2.Id
            }
        };

        return Ok(retVal);
    }

}
