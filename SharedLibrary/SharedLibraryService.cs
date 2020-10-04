using Microsoft.Azure.Devices;
using System.Threading.Tasks;

//OBS! Denna projekt är till testet

namespace SharedLibrary
{
    public class SharedLibraryService
    {
        private ServiceClient serviceClient;

        public SharedLibraryService(string connectionstring)
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionstring);
        }


        
        public async Task<CloudToDeviceMethodResult> InvokeMethodAsync(string targetDevice, string methodName, string payload)
        {
            //skickar payload (json) och exekverar en direct method på devicen
            var send = new CloudToDeviceMethod(methodName).SetPayloadJson(payload);
            //anropar en method på devicen 
            return await serviceClient.InvokeDeviceMethodAsync(targetDevice, send);
        }
    }
}
