using Microsoft.AspNetCore.Components;
using PublicTransportRealtime.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicTransportRealtime.Shared
{
    public partial class SubscriptionComponent
    {
        [Parameter] public EndpointConfiguration Configuration { get; set; }
    }
}
