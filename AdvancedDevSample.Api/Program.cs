using AdvancedDevSample.Api.Middlewares;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Interfaces.Customers;
using AdvancedDevSample.Domain.Interfaces.Oders;
using AdvancedDevSample.Domain.Interfaces.Products;
using AdvancedDevSample.Domain.Interfaces.Suppliers;
using AdvancedDevSample.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;

    foreach (var xmlFile in Directory.GetFiles(basePath, "*.xml"))
    {
        options.IncludeXmlComments(xmlFile);
    }
});

// Services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<OrderService>();

// Repositories
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<ICustomerRepository, InMemoryCustomerRepository>();
builder.Services.AddScoped<ISupplierRepository, InMemorySupplierRepository>();
builder.Services.AddScoped<IOrderRepository, InMemoryOrderRepository>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // Clients
    var customerRepo = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
    if (customerRepo is InMemoryCustomerRepository custRepo)
        custRepo.Seed();

    // Fournisseurs
    var supplierRepo = scope.ServiceProvider.GetRequiredService<ISupplierRepository>();
    if (supplierRepo is InMemorySupplierRepository suppRepo)
        suppRepo.Seed();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseMiddleware<ExceptionHandLingMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program();