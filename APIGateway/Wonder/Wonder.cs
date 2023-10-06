using Ocelot.ServiceDiscovery.Providers;
using Ocelot.Values;

namespace APIGateway.Wonder
{
    public class Wonder:IServiceDiscoveryProvider
    {
        readonly IInternalWonderServiceRepository repository;
        readonly string serviceName;

        public Wonder(IInternalWonderServiceRepository repository, string serviceName)
        {
            this.repository = repository;
            this.serviceName = serviceName;
        }

        public Task<List<Service>> Get()
        {
            return Task.FromResult(repository.GetService(serviceName));
        }

    }
}
