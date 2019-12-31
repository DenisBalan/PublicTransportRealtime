using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace PublicTransportRealtime.Pages
{
    public class IndexModel: ComponentBase, IDisposable
    {
        [Inject] JSRuntime _clientRuntime { get; set; }

        public void Dispose()
        {
        }
    }
}
