using AdvancedDevSample.Application.DTOs;
using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces.Products;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace AdvancedDevSample.Tests.API.Integration
{
    public class ProductAsyncControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly InMemoryProductRepository? _repo;

        public ProductAsyncControllerIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _repo = factory.Services.GetRequiredService<IProductRepository>() as InMemoryProductRepository;
        }

        [Fact]
        public async Task ChangePrice_Should_Return_NoContent_And_Save_Product()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product(
                id: productId,
                name: "Test Product",
                price: 10m,
                isActive: true
            );

            _repo.Seed(product);

            var request = new ChangePriceRequest { NewPrice = 20 };

            // Act
            var response = await _client.PutAsJsonAsync(
                $"/api/products/{productId}/price",
                request
            );

            // Assert - HTTP
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Assert - Persistance
            var updatedProduct = _repo.GetById(productId);
            Assert.NotNull(updatedProduct);
            Assert.Equal(20, updatedProduct.Price);
        }

        [Fact]
        public async Task ChangePrice_Should_Return_NotFound_For_Invalid_Id()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var request = new ChangePriceRequest { NewPrice = 20 };

            // Act
            var response = await _client.PutAsJsonAsync(
                $"/api/products/{invalidId}/price",
                request
            );

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ChangePrice_Should_Return_BadRequest_For_Invalid_Price()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product(productId, "Test", 10m, true);
            _repo.Seed(product);

            var request = new ChangePriceRequest { NewPrice = -10 }; // Prix invalide

            // Act
            var response = await _client.PutAsJsonAsync(
                $"/api/products/{productId}/price",
                request
            );

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}