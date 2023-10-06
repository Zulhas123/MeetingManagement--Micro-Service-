using Ocelot.Responses;

namespace APIGateway.Wonder
{
    public interface IServiceFileRepository
    {
        Task<Response<List<ServiceRegistration>>> Get();
        Task<Response> Set(List<ServiceRegistration> services);
    }
}
