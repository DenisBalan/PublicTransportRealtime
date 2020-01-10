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
    public partial class Index : ComponentBase
    {
        public SubscriptionModel[] Subscriptions { get; set; }
        public Func<SubscriptionModel[]> ActiveSubscriptions => () => Subscriptions.Where(k => k.IsActive).ToArray();
        [Inject] IJSRuntime ClientRuntime { get; set; }
        [Inject] TransportDataProviderService Service { get; set; }
        [Inject] ILocalStorageService LocalStorage { get; set; }
        private bool canRunJs;
        protected override async Task OnInitializedAsync()
        {
            Subscriptions = (await Service.GetRouteData())
                .Select(SubscriptionModel.FromRouteData).ToArray();
            Service.OnTelemetryArrived += Service_OnTelemetryArrived;
            await Task.Run(Service.StartProvidingTelemetry);
        }

        private async void Service_OnTelemetryArrived(object sender, TransportLocationData e)
        {
            if (!canRunJs) return;
            await ClientRuntime.InvokeVoidAsync("console.log", new[] { e });
        }

        public async Task RouteChecked(SubscriptionModel route)
        {
            await ClientRuntime.InvokeVoidAsync("handleActiveRoutes", new[] { ActiveSubscriptions() });
            await LocalStorage.SetItemAsync("all", ActiveSubscriptions());
            _ = Task.Run(() => Service.OnSubscriptionListChanged.Invoke(route));
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
            canRunJs = true;
            StateHasChanged();
        }
    }
}
