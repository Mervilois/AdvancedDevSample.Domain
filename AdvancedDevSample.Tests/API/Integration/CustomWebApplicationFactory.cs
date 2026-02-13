using AdvancedDevSample.Domain.Interfaces.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AdvancedDevSample.Api;
using System;

namespace AdvancedDevSample.Tests.API.Integration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remplacer le repository par la version in-memory
                services.RemoveAll<IProductRepository>();
                services.AddSingleton<IProductRepository, InMemoryProductRepository>();
            });
        }
    }
}