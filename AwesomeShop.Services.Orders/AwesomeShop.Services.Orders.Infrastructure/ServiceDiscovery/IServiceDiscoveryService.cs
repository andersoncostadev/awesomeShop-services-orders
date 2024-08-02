using System;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Orders.Infrastructure.ServiceDiscovery
{
    public interface IServiceDiscoveryService
    {
        Task<Uri> GetServiceDiscoveryUri(string serviceName, string requestUrl);
    }
}
