using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Stateless1.Interfaces;

namespace Stateless1
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Stateless1 : StatelessService, IStateless1
    {
        private readonly Uri _serviceUri;

        public Stateless1(StatelessServiceContext context)
            : base(context)
        {
            _serviceUri = context.ServiceName;
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return this.CreateServiceRemotingInstanceListeners();
        }

        /// <summary>
        /// This method is called as the final step of opening the service.
        /// Override this method to be notified that Open has completed for this instance's internal components.
        /// <para>
        /// For information about Reliable Services life cycle please see
        /// https://docs.microsoft.com/azure/service-fabric/service-fabric-reliable-services-lifecycle
        /// </para>
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task">Task</see> that represents outstanding operation.
        /// </returns>
        protected override async Task OnOpenAsync(CancellationToken cancellationToken)
        {
            var client = new FabricClient();
            await client.PropertyManager.PutPropertyAsync(_serviceUri, "ServiceName", "true");
        }
        
        public async Task<string> DoSomething(string words)
        {
            return $"{words} and so on";
        }
    }
}
