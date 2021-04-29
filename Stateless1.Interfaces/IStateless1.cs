using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport;

[assembly:FabricTransportServiceRemotingProvider(RemotingClientVersion = RemotingClientVersion.V2_1, RemotingListenerVersion = RemotingListenerVersion.V2_1, OperationTimeoutInSeconds = 15)]
namespace Stateless1.Interfaces
{
	public interface IStateless1 : IService
	{
		Task<string> DoSomething(string words);
	}
}
