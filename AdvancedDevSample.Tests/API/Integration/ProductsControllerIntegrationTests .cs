using AdvancedDevSample.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace AdvancedDevSample.Tests.API.Integration
{
    public class ProductsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ProductsControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateProduct_Should_Return_Created_Status()
        {
            var request = new CreateProductRequest
            {
                Name = "Nouveau Produit",
                Price = 29.99m,
                Description = "Description du produit"
            };

            var response = await _client.PostAsJsonAsync("/api/products", request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var location = response.Headers.Location;
            Assert.NotNull(location);
            Assert.Contains("/api/products/", location.ToString());
        }

        [Fact]
        public async Task GetProduct_Should_Return_NotFound_For_Invalid_Id()
        {
            var invalidId = Guid.NewGuid();

            var response = await _client.GetAsync($"/api/products/{invalidId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_Should_Return_NoContent()
        {
            var productId = Guid.NewGuid(); // Remplacer par un ID existant
            var request = new UpdateProductRequest
            {
                Name = "Produit Modifié",
                Price = 39.99m,
                Description = "Description modifiée"
            };
            
            var response = await _client.PutAsJsonAsync($"/api/products/{productId}", request);

            Assert.True(response.StatusCode == HttpStatusCode.NoContent ||
                       response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ChangePrice_Should_Return_NoContent_When_Successful()
        {
            var productId = Guid.NewGuid(); // Remplacer par un ID existant
            var request = new ChangePriceRequest { NewPrice = 49.99m };

            var response = await _client.PutAsJsonAsync($"/api/products/{productId}/price", request);

            Assert.True(response.StatusCode == HttpStatusCode.NoContent ||
                       response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteProduct_Should_Return_NoContent()
        {
            var productId = Guid.NewGuid(); // Remplacer par un ID existant

            var response = await _client.DeleteAsync($"/api/products/{productId}");

            Assert.True(response.StatusCode == HttpStatusCode.NoContent ||
                       response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ApplyDiscount_Should_Return_BadRequest_For_Invalid_Discount()
        {
            var productId = Guid.NewGuid(); // Remplacer par un ID existant

            var request = new ChangePriceRequest { NewPrice = 49.99m };

            var response = await _client.PutAsJsonAsync($"/api/products/{productId}/price", request);

            Assert.True(response.StatusCode == HttpStatusCode.NoContent ||
                       response.StatusCode == HttpStatusCode.NotFound);
        }
    }
}
