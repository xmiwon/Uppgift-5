using Microsoft.Azure.Devices.Client;
using System;
using System.Text;
using System.Threading.Tasks;
namespace SharedLibrary
{
    public class SharedLibraryDevice
    {
        public DeviceClient deviceClient;
        private int _telemetryInterval = 5;
        private readonly string connectionString = "HostName=ec-win20-iothub-mw.azure-devices.net;DeviceId=DeviceApp;SharedAccessKey=GT0cvCoqiGi8qqBW5dKoFcFx7pnP7ClLQa2mOGVwmRA=";


        public SharedLibraryDevice()
        {
            deviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);
            
            deviceClient.SetMethodHandlerAsync("ChangeInterval", SetTelemetryIntervalAsync, null).GetAwaiter();
            
        }

        //Försök att lägga in intervallet och returnera true, annars false
        public bool ChangeInterval(int interval)
        {
            try
            {
                _telemetryInterval = interval;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<MethodResponse> SetTelemetryIntervalAsync(MethodRequest request, object userContext)
        {
            //konvertera json värde till en int - slipper skapa json och konvertera fram och tillbaka
            var convert = Convert.ToInt32(request.DataAsJson);
            //startar funktionen med värdet som finns i convert (int)
            ChangeInterval(convert);
            //Trigger en aktivitet hos devicen. Gör en instance som tar emot json till bytes med stöf för utf8 och skickar status code 200
            var response = new MethodResponse(Encoding.UTF8.GetBytes(request.DataAsJson), 200);
            return await Task.FromResult(response);

        }
    }
}
