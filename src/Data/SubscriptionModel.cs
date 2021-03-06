﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace PublicTransportRealtime.Data
{
    public class SubscriptionModel: IComparable<SubscriptionModel>, IEqualityComparer<SubscriptionModel>
    {
        // another way of doing EDA
        // remove @onclick="(() => OnChecked(route))" from Component
        // private bool _isActive;
        // public bool IsActive { get { return _isActive; } set { _isActive = value; OnChange?.Invoke(this, this);  } }
        // public event EventHandler<SubscriptionModel> OnChange;

        public bool IsActive { get; set; }
        public RouteDataInformation RouteData { get; set; }

        public static SubscriptionModel FromRouteData(RouteDataInformation rd)
        => new SubscriptionModel
        {
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
