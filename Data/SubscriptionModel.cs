using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace PublicTransportRealtime.Data
{
    public class SubscriptionModel: IComparable<SubscriptionModel>, IEqualityComparer<SubscriptionModel>
    {
        public bool IsActive { get; set; }
        public RouteData RouteData { get; set; }

        public static SubscriptionModel FromRouteData(RouteData rd, bool isSelected)
        => new SubscriptionModel
        {
            IsActive = isSelected,
            RouteData = rd
        };

        public int CompareTo(SubscriptionModel other)
        {
            return this.RouteData.MqttId - other.RouteData.MqttId;
        }

        public bool Equals([AllowNull] SubscriptionModel x, [AllowNull] SubscriptionModel y)
        {
            return x.CompareTo(y).Equals(0);
        }

        public int GetHashCode([DisallowNull] SubscriptionModel obj)
        {
            return obj.RouteData.MqttId;
        }
    }
}
