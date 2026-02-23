using DemoOptionsPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace DemoOptionsPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoApiController : ControllerBase
    {
        private readonly ILogger<DemoApiController> _logger;
        private readonly MyAppOptionsModel _options;
        private readonly MyAppOptionsModel _optionsSnapshot;
        private readonly MyAppOptionsModel _optionsMonitor;

        public DemoApiController(
            ILogger<DemoApiController> logger,
            IOptions<MyAppOptionsModel> options,
            IOptionsSnapshot<MyAppOptionsModel> optionsSnapshot,
            IOptionsMonitor<MyAppOptionsModel> optionsMonitor)
        {
            _logger = logger;

            _options = options.Value;
            _optionsSnapshot = optionsSnapshot.Value;
            _optionsMonitor = optionsMonitor.CurrentValue;
        }


        // NOTE:
        // Run the application, and while the application is running, change the value in appsettings.json file.
        // options will not reflect the change, but the other two would!
        // GET https://localhost:7091/api/myappoptionsapi/myappoptions
        [HttpGet(template: "myappoptions")]
        [EndpointSummary(summary: "Demo of Options Pattern")]
        [EndpointDescription(description: "Run the application, and while the application is running, change the value in appsettings.json file.")]
        public IActionResult GetFromOptionsPattern()
        {

            var response = new
            {
                // IOptions interface loads the configuration values only once, during the application startup.
                // Use IOptions when the configuration values are not expected to change.
                options = new
                {
                    _options.CompanyName,
                    _options.TrainerId,
                    _options.TrainerName
                },

                // IOptionsSnapshot is a SCOPED SERVICE that gives a snapshot of options at the time the constructor is invoked.
                // Use IOptionsSnapshot values are expected to change, but you want them to be uniform for the entire request cycle.
                optionsSnapshot = new
                {
                    _optionsSnapshot.CompanyName,
                    _optionsSnapshot.TrainerId,
                    _optionsSnapshot.TrainerName
                },

                // IOptionsMonitor is a SINGLETON SERVICE that gets the current value at any time.
                // Use IOptionsMonitor when you need real-time options data.
                optionsMonitor = new
                {
                    _optionsMonitor.CompanyName,
                    _optionsMonitor.TrainerId,
                    _optionsMonitor.TrainerName
                }
            };


            return Ok(response);
        }

    }
}
