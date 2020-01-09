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
    public partial class Index : ComponentBase, IDisposable
    {
        public SubscriptionModel[] Subscriptions { get; set; }
        public SubscriptionModel[] ActiveSubscriptions => Subscriptions.Where(k => k.IsActive).ToArray();
        [Inject] IJSRuntime ClientRuntime { get; set; }
        [Inject] TransportDataProviderService Service { get; set; }
        public SockJS SocketJsClient { get; set; }
        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                SocketJsClient?.Close();
            }
        }
        

    }
}
