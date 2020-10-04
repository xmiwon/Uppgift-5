using Xunit;
using SharedLibrary;



//OBS! Denna projekt �r till testet

namespace ConsoleApp.Tests
{
    public class ConsoleAppTest
    {
        private readonly string connectionString = "HostName=ec-win20-iothub-mw.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=3pKef/FGjE4rEyAipCl2EkunC59xOLc7W8vUBUzfc8w=";


        //H�r testas om service appen ger tillbaka status koden 200 eller 501 om method namnet finns
        [Theory]
        [InlineData("DeviceApp", "ChangeInterval", "5", "200")]
        [InlineData("DeviceApp", "GetInterval", "5", "501")]

        public void Test1(string targetDevice, string methodName, string payload, string expected)
        {
            var response = 
                new SharedLibraryService(connectionString)
                .InvokeMethodAsync(targetDevice, methodName, payload);
        
            
            // �r response.result.status "200" (string) samma som expected "200"?
            Assert.Equal(expected, response.Result.Status.ToString());


        }

        
        //H�r testas Device appen om intervallen som s�tts returneras true v�rde
        [Fact]
        public void Test2()
        {
            int value1 = 5;
       

            var test1 = new SharedLibraryDevice().ChangeInterval(value1);
            //konverterar till string f�r att ska kunna testa mot "True" som �r en string
            Assert.Equal("True", test1.ToString());
        }




    }
}
