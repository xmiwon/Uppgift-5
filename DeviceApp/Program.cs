using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApp
{
    class Program
    {
        private static DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=ec-win20-iothub-mw.azure-devices.net;DeviceId=DeviceApp;SharedAccessKey=GT0cvCoqiGi8qqBW5dKoFcFx7pnP7ClLQa2mOGVwmRA=", TransportType.Mqtt);
        private static int telemetryInterval = 5; //intervallet för mellan varje meddelanden
        private static Random rnd = new Random();
        static void Main(string[] args)
        {
            //den ska leta efter ordet  settelemetryinterval, när den hittar så ska den köra funktionen settelemetryinterval
            deviceClient.SetMethodHandlerAsync("SetTelemetryInterval", SetTelemetryInterval, null).GetAwaiter();
            SendMessageAsync().GetAwaiter();

            Console.ReadKey();

        }
        // ALLT DENNA FUNKTION GÖR ÄR ATT ÄNDRA INTERVALLET OCH RESPOND MED 200
        private static Task<MethodResponse> SetTelemetryInterval(MethodRequest request, object userContext)
        {
            var payload = Encoding.UTF8.GetString(request.Data).Replace("\"","");
            Console.WriteLine(payload + "aaa");
            //testar att parsa till en int och kommer ge true eller false värde. Om true, skickar UT (out) till telemetryInterval (assign)
            //out gör att att man kan sätta en befintligt variable utanför denna scope
            if(Int32.TryParse(payload, out telemetryInterval))
            {
                Console.WriteLine($"Interval set to: {telemetryInterval} seconds.");
                // {"result": "Executed direct method: SetTelemetryInterval" }
                string json = "{\"result\": \"Executed direct method: " + request.Name + "\"}";
                var response = new MethodResponse(Encoding.UTF8.GetBytes(json), 200);
              
                return Task.FromResult(response);
            }
            else
            {
                string json = "{\"result\": \"Method not implemented:\"}";

                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(json), 501));
            }
        }
       
        
        
        private static async Task SendMessageAsync()
        {
            //evighets loop som slumpar fram nya värden för temp och hum som sedan läggs till en objekt
            while (true)
            {
            double temp = 10 + rnd.NextDouble() * 15;
            double hum = 40 + rnd.NextDouble() * 20;
            var data = new
            {
                temperature = temp,
                humidity = hum
            };
            var json = JsonConvert.SerializeObject(data);
                //skickar meddelanden som json och skickar en notis till hubben om temp överstiger => true else false
            var payload = new Message(Encoding.UTF8.GetBytes(json));
            payload.Properties.Add("temperatureAlert", (temp > 10) ? "true" : "false");

            await deviceClient.SendEventAsync(payload);
            Console.WriteLine($"Message sent: {json}");

            await Task.Delay(telemetryInterval * 1000);
            }
        }
    }
}
