using Blazored.LocalStorage;
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
        public Func<SubscriptionModel[]> ActiveSubscriptions => () => Subscriptions.Where(k => k.IsActive).ToArray();
        [Inject] IJSRuntime ClientRuntime { get; set; }
        [Inject] TransportDataProviderService Service { get; set; }
        //[Inject] 
        SockJS SocketJsClient { get; set; }
        [Inject] ILocalStorageService LocalStorage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Subscriptions = (await Service.GetRouteData())
                .Select(SubscriptionModel.FromRouteData).ToArray();
        }

        public async Task RouteChecked(SubscriptionModel route)
        {
            await ClientRuntime.InvokeVoidAsync("handleActiveRoutes", new[] { ActiveSubscriptions() });
            await LocalStorage.SetItemAsync("all", ActiveSubscriptions());
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;
            var routesFromClient = await LocalStorage.GetItemAsync<SubscriptionModel[]>("all") ?? Enumerable.Empty<SubscriptionModel>();
            var realObjects = routesFromClient.ToDictionary(h => h, h => Subscriptions.FirstOrDefault(f => f.Equals(f, h)));

            realObjects.ToList().ForEach(async (x) =>
            {
                // PATCH, make runtime object properties like in saved from localstorage
                x.Value.IsActive = x.Key.IsActive;
                await RouteChecked(x.Value);
            });

            StateHasChanged();
        }

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
