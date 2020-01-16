using Microsoft.AspNetCore.Components;
using PublicTransportRealtime.Data;
using PublicTransportRealtime.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicTransportRealtime.Shared
{
    public partial class SubscriptionComponent
    {
        [Parameter]
        public SubscriptionModel[] Subscriptions { get; set; }
        [Parameter] 
        public EventCallback<SubscriptionModel> OnSelectionChange { get; set; }


        public async Task OnChecked(SubscriptionModel v)
        {
            await OnSelectionChange.InvokeAsync(v);
        }
    }
}
