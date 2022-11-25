using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using EscortBookAuthorizer.Consumer.Extensions;
using EscortBookAuthorizer.Consumer.Repositories;
using EscortBookAuthorizer.Consumer.Backgrounds;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMongoDBClient();
builder.Services.AddSingleton<IAccessTokenRepository, AccessTokenRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddHostedService<KafkaAuthorizerConsumer>();

var app = builder.Build();

app.UseHttpLogging();
app.MapControllers();
app.Run();
