using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using PublicTransportRealtime.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicTransportRealtime.Services
{
    public class TransportDataProviderService
    {
        public TransportDataProviderService(IOptions<EndpointConfiguration> EndpointConfiguration)
        {
            this.EndpointConfiguration = EndpointConfiguration;
        }
        public IOptions<EndpointConfiguration> EndpointConfiguration { get; set; }
        public async Task<RouteDataInformation[]> GetRouteData()
        {
            await Task.Yield();
            return EndpointConfiguration.Value.Routes;
        }
    }
}
