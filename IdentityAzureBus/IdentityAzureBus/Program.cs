using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Azure.Identity;
using System.Reflection;
using IdentityAzureBus.Commands;
using Microsoft.Extensions.Options;
using static IdentityAzureBus.Queries.QueryHandler;
using IdentityAzureBus.Events;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//               .(options =>
//               {
//                   options.ClientId = builder.GetContext().Configuration["AzureAd:ClientId"];
//                   options.TenantId = builder.GetContext().Configuration["AzureAd:TenantId"];
//               });

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    options.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
    options.RegisterServicesFromAssembly(typeof(GetUserQueryHandler).Assembly);
    options.RegisterServicesFromAssembly(typeof(UserCreatedEventHandler).Assembly);
});

builder.Build().Run();
