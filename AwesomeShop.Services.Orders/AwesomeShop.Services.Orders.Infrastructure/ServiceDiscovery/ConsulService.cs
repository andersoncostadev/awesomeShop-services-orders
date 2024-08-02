using Consul;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Orders.Infrastructure.ServiceDiscovery
{
    public class ConsulService : IServiceDiscoveryService
    {
        private readonly IConsulClient _consulClient;

        public ConsulService(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }

        public async Task<Uri> GetServiceDiscoveryUri(string serviceName, string requestUrl)
        {
            var allRegisteredServices = await _consulClient.Agent.Services();

            var registeredService = allRegisteredServices.Response?.Where(s => s.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                .Select(s => s.Value)
                .ToList();

            var service = registeredService.FirstOrDefault();

            Console.WriteLine($"Found service {service.Service} at {service.Address}:{service.Port}");

            var uri = $"http://{service.Address}:{service.Port}{requestUrl}";

            return new Uri(uri);
        }
    }
}
