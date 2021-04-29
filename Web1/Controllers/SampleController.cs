using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using Stateless1.Interfaces;

namespace Web1.Controllers
{
    [ApiController]
    [Route("sample")]
    public class SampleController : ControllerBase
    {
        [HttpGet("one")]
        public async Task<string> Stateless1([FromQuery]string words)
        {
            var uri = new Uri("fabric:/Sf01/Stateless1");

            //Doesn't work
            // var proxyFactory = new ServiceProxyFactory(_ => new FabricTransportServiceRemotingClientFactory(),
            //     new OperationRetrySettings(TimeSpan.FromSeconds(15)));
            // var stateless1 = proxyFactory.CreateServiceProxy<IStateless1>(uri, ServicePartitionKey.Singleton);

            //Works
            var stateless1 = ServiceProxy.Create<IStateless1>(uri, ServicePartitionKey.Singleton);
            
            return await stateless1.DoSomething(words);
        }
    }
}
