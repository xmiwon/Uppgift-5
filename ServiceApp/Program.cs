using Microsoft.Azure.Devices;
using System;
using System.Threading.Tasks;
//vg delen
namespace ServiceApp
{
    class Program
    {
        private static ServiceClient serviceClient = ServiceClient.CreateFromConnectionString("HostName=ec-win20-iothub-mw.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=3pKef/FGjE4rEyAipCl2EkunC59xOLc7W8vUBUzfc8w=");



        static void Main(string[] args)
        {
            Task.Delay(5000).Wait();
            // anropar en metod på objektet från device sidan med targetid, method namnet och payload
            InvokeMethod("DeviceApp", "SetTelemetryInterval", "10").GetAwaiter();
            Console.ReadKey();
        }


        //Den här delen motsvarar samma del som i Device explorer "Call Method on Device"
            static async Task InvokeMethod(string deviceId, string methodName, string payload)
            {
            
            var methodInvocation = new CloudToDeviceMethod(methodName) { ResponseTimeout = TimeSpan.FromSeconds(30) };
                methodInvocation.SetPayloadJson(payload);
                var response = await serviceClient.InvokeDeviceMethodAsync(deviceId, methodInvocation);


                Console.WriteLine($"Response Status: {response.Status}");
                Console.WriteLine(response.GetPayloadAsJson());
            }

    }
}
