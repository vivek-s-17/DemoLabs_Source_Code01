using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApi.Tests
{
    public class UnitTest1
    {

        private readonly ITestOutputHelper _outputHelper;

        public UnitTest1(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }


        [Fact]
        public void Test1_SyncVersion()
        {
            // ---- 1. ARRANGE
            var controller = new DemoWebApi.Controllers.ApiTestController();
            string expectedResult = "hello world from my API";

            // ----- 2. ACT
            var responseObject = controller.Get();            // action ResultObject (IActionResult)

            // ----- 3(a). ASSERT (check if OkObjectResult is received)
            Assert.IsType<OkObjectResult>(responseObject);

            // ----- 3(b). ASSERT (check if ResponseCode is 200)
            var okResultObject = responseObject as OkObjectResult;
            Assert.Equal(expected: StatusCodes.Status200OK, actual: okResultObject?.StatusCode);

            // ----- 3(c). ASSERT (check if the correct responseObject is received)
            var actualResult = okResultObject!.Value;           // I know for sure it is not null.  Hence "!."
            Assert.Equal(expected: expectedResult, actualResult);

            _outputHelper.WriteLine("Successful!");
        }


        [Fact]
        public async Task Test2_AsyncVersion()
        {
            // ---- 1. ARRANGE
            var controller = new DemoWebApi.Controllers.ApiTestController();
            string expectedResult = "hello world from my API";

            // ----- 2. ACT
            var responseObject = await controller.GetAsync();        // action ResultObject from Task<IActionResult>

            // ----- 3(a). ASSERT (check if OkObjectResult is received)
            Assert.IsType<OkObjectResult>(responseObject);

            // ----- 3(b). ASSERT (check if ResponseCode is 200)
            var okResultObject = responseObject as OkObjectResult;
            Assert.Equal(expected: StatusCodes.Status200OK, actual: okResultObject?.StatusCode);

            // ----- 3(c). ASSERT (check if the correct responseObject is received)
            var actualResult = okResultObject!.Value;           // I know it for sure it is not null.  Hence "!."
            Assert.Equal(expected: expectedResult, actualResult);
        }

    }
}
