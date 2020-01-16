using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using PublicTransportRealtime.Data;
using syp.biz.SockJS.NET.Client;
using syp.biz.SockJS.NET.Client.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicTransportRealtime.Services
{
    public class TransportDataProviderService
    {
        public event EventHandler<TransportLocationData> OnTelemetryArrived;
        public Action<SubscriptionModel> OnSubscriptionListChanged;
        public HashSet<SubscriptionModel> SubscriptionModels { get; set; } = new HashSet<SubscriptionModel>();
        public IOptions<EndpointConfiguration> EndpointConfiguration { get; }
        public SockJS SockJS { get; private set; }
        private bool canSend;
        public TransportDataProviderService(IOptions<EndpointConfiguration> EndpointConfiguration)
        {
            this.EndpointConfiguration = EndpointConfiguration;
            OnSubscriptionListChanged = (b) => { SubscriptionModels.Add(b); ProcessChanges(b); };
        }
        public void StartProvidingTelemetry()
        {
            SockJS = new SockJS(EndpointConfiguration.Value.Url);
            SockJS.AddEventListener("open", (sender, e) =>
            {
                SockJS.Send(EndpointConfiguration.Value.ConnectionString);
            });
            SockJS.AddEventListener("message", (sender, obj) =>
            {
                if (obj[0] is TransportMessageEvent msg)
                {
                    var dataString = msg.Data.ToString();
                    if (dataString.Contains("No subscription found")) return;
                    var conn = dataString.StartsWith("CONNECTED");
                    canSend |= conn;
                    if (conn)
                    {
                        SubscriptionModels.ToList().ForEach(ProcessChanges);
                    }
                    var kk = TransportLocationData.FromJson(dataString);
                    if (kk is null) return;
                    OnTelemetryArrived?.Invoke(this, kk);
                }
            });
            SockJS.AddEventListener("close", (sender, e) =>
            {
                Task.Run(StartProvidingTelemetry);
            });
        }

        private void ProcessChanges(SubscriptionModel model)
        {
            if (!canSend) return;
            var state = "UNSUBSCRIBE";
            if (model.IsActive) { state = state.Substring(2); }
            SockJS.Send(
               $"{state}\nid:sub-1577130075933-8{model.RouteData.MqttId}\ndestination:/exchange/e_public_rtec_Sho0ohCiephoh2waeM9t" +
               $"/telemetry.route.{model.RouteData.MqttId}\n\n\u0000"
               );
        }

        public async Task<RouteDataInformation[]> GetRouteData()
        {
            await Task.Yield();
            return EndpointConfiguration.Value.Routes;
        }
    }
}
