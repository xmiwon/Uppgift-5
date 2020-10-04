using Xunit;
using SharedLibrary;



//OBS! Denna projekt är till testet

namespace ConsoleApp.Tests
{
    public class ConsoleAppTest
    {
        private readonly string connectionString = "HostName=ec-win20-iothub-mw.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=3pKef/FGjE4rEyAipCl2EkunC59xOLc7W8vUBUzfc8w=";


        //Här testas om service appen ger tillbaka status koden 200 eller 501 om method namnet finns
        [Theory]
        [InlineData("DeviceApp", "ChangeInterval", "5", "200")]
        [InlineData("DeviceApp", "GetInterval", "5", "501")]

        public void Test1(string targetDevice, string methodName, string payload, string expected)
        {
            var response = 
                new SharedLibraryService(connectionString)
                .InvokeMethodAsync(targetDevice, methodName, payload);
        
            
            // Är response.result.status "200" (string) samma som expected "200"?
            Assert.Equal(expected, response.Result.Status.ToString());


        }

        
        //Här testas Device appen om intervallen som sätts returneras true värde
        [Fact]
        public void Test2()
        {
            int value1 = 5;
       

            var test1 = new SharedLibraryDevice().ChangeInterval(value1);
            //konverterar till string för att ska kunna testa mot "True" som är en string
            Assert.Equal("True", test1.ToString());
        }




    }
}
