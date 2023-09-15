using Xunit.Abstractions;

namespace HowToImplementTests.UnitTesting
{
    public class UnitTest1
    {
        private ITestOutputHelper _testOutputHelper { get; }


        //setup goies here
        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        

        [Fact]
        public void Test1()
        {
            //Arrange
            _testOutputHelper.WriteLine("Demo");

            //Act


            //Assert

        }
    }
}