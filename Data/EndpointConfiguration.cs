using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicTransportRealtime.Data
{
    public class EndpointConfiguration
    {
        public string Url { get; set; }
        public RouteData[] Routes { get; set; }
    }
}
