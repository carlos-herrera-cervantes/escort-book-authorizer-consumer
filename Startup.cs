using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using EscortBookAuthorizerConsumer.Extensions;
using Microsoft.Extensions.Configuration;
using EscortBookAuthorizerConsumer.Backgrounds;
using EscortBookAuthorizerConsumer.Repositories;

namespace EscortBookAuthorizerConsumer;

public class Startup
{
    private IConfiguration Configuration { get; set; }

    public Startup(IConfiguration configuration) => Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMongoDBClient();
        services.AddSingleton<IAccessTokenRepository, AccessTokenRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddHostedService<KafkaAuthorizerConsumer>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { }
}
