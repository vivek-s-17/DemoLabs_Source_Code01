namespace DemoWebApiDB.Tests
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }


        [Fact]
        public void Test1_ShouldReturn_CorrectResult()
        {
            // 1. ARRANGE
            int a = 10;                         // deterministic test-data
            int b = 20;                         // deterministic test-data
            int expectedResult = 30;            // deterministic test-result
            int actualResult;

            _output.WriteLine("preparation completed for First test");

            // 2. ACT (run the test)
            actualResult = a + b;

            _output.WriteLine("completed running the test");
            _output.WriteLine("Actual Result {0}, Expected Result: {1}", expectedResult, actualResult);

            // 3. ASSERT
            Assert.Equal(expectedResult, actualResult);
        }



        [Fact]
        public void Test1_ShouldReturn_InvalidResult()
        {
            // 1. ARRANGE
            int a = 10;                         // deterministic test-data
            int b = 30;                         // deterministic test-data
            int expectedResult = 30;            // deterministic test-result
            int actualResult;

            // 2. ACT (run the test)
            actualResult = a + b;

            // 3. ASSERT
            Assert.NotEqual(expectedResult, actualResult);
        }

    }
}
