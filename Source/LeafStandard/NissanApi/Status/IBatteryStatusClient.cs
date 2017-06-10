using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeafStandard.NissanApi.Status
{
    public interface IBatteryStatusClient
    {
        Task<BatteryStatusResponse> GetStatusAsync(string sessionId, CancellationToken cancelToken);
    }
}
