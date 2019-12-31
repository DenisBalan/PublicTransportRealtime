using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using PublicTransportRealtime.Data;
using PublicTransportRealtime.Services;
using syp.biz.SockJS.NET.Client;
using syp.biz.SockJS.NET.Client.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicTransportRealtime.Pages
{
    public partial class Index: ComponentBase, IDisposable
    {
        [Inject] IOptions<EndpointConfiguration> EndpointConfiguration { get; set; }
        [Inject] IJSRuntime ClientRuntime { get; set; }
        [Inject] TransportDataProvider Service { get; set; }
        public SockJS SockJs { get; set; }
        public void Dispose()
        {
            SockJs?.Close();
        }
    }
}
