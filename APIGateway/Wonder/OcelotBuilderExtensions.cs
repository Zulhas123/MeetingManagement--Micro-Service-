using Ocelot.DependencyInjection;

namespace APIGateway.Wonder
{
    public static class OcelotBuilderExtensions
    {
        public static IOcelotBuilder AddWonder(this IOcelotBuilder builder)
        {
            builder.Services.AddSingleton(WonderProviderFactory.Get);
            builder.Services.AddSingleton(WonderMiddlewareConfigurationProvider.Get);
            builder.Services.AddSingleton<IServiceFileRepository, ServiceFileRepository>();
            builder.Services.AddSingleton<IInternalWonderServiceRepository, InternalWonderServiceRepository>();
            return builder;
        }
    }
}
