using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Exceptions;
using AdvancedDevSample.Tests.Application.Fakes;
using System;
using System.Linq;
using Xunit;

namespace AdvancedDevSample.Tests.Application.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public void CreateProduct_Should_Return_Valid_Guid()
        {
            // Arrange
            var repo = new FakeProductRepository();
            var service = new ProductService(repo);

            // Act
            var productId = service.CreateProduct("Nouveau Produit", 15.99m, "Description test");

            // Assert
            Assert.NotEqual(Guid.Empty, productId);
            Assert.True(repo.Exists(productId));
        }

        [Fact]
        public void GetProduct_Should_Return_Product_When_Exists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var expectedProduct = new Product(productId, "Test Product", 10.99m, true, "Description");
            var repo = new FakeProductRepository(expectedProduct);
            var service = new ProductService(repo);

            // Act
            var result = service.GetProduct(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal("Test Product", result.Name);
        }

        [Fact]
        public void GetProduct_Should_Throw_When_Product_Not_Found()
        {
            // Arrange
            var repo = new FakeProductRepository(); // Repository vide
            var service = new ProductService(repo);
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ApplicationServiceException>(() => service.GetProduct(nonExistentId));
        }

        [Fact]
        public void GetAllProducts_Should_Return_All_Products()
        {
            // Arrange
            var repo = new FakeProductRepository();
            repo.AddProduct(new Product(Guid.NewGuid(), "Produit 1", 10.99m, true));
            repo.AddProduct(new Product(Guid.NewGuid(), "Produit 2", 20.99m, true));
            repo.AddProduct(new Product(Guid.NewGuid(), "Produit 3", 30.99m, false));

            var service = new ProductService(repo);

            // Act
            var products = service.GetAllProducts().ToList();

            // Assert
            Assert.Equal(3, products.Count);
        }

        [Fact]
        public void GetActiveProducts_Should_Return_Only_Active_Products()
        {
            // Arrange
            var repo = new FakeProductRepository();
            var activeProduct = new Product(Guid.NewGuid(), "Produit Actif", 10.99m, true);
            var inactiveProduct = new Product(Guid.NewGuid(), "Produit Inactif", 20.99m, false);

            repo.AddProduct(activeProduct);
            repo.AddProduct(inactiveProduct);

            var service = new ProductService(repo);

            // Act
            var activeProducts = service.GetActiveProducts().ToList();

            // Assert
            Assert.Single(activeProducts);
            Assert.Equal("Produit Actif", activeProducts[0].Name);
        }

        [Fact]
        public void UpdateProduct_Should_Update_Product_Details()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product(productId, "Ancien Nom", 10.99m, true, "Ancienne Description");
            var repo = new FakeProductRepository(product);
            var service = new ProductService(repo);

            // Act
            service.UpdateProduct(productId, "Nouveau Nom", 19.99m, "Nouvelle Description");

            // Assert
            var updatedProduct = repo.GetById(productId);
            Assert.NotNull(updatedProduct);
            Assert.Equal("Nouveau Nom", updatedProduct.Name);
            Assert.Equal(19.99m, updatedProduct.Price);
            Assert.Equal("Nouvelle Description", updatedProduct.Description);
            Assert.True(repo.WasSaved);
        }

        [Fact]
        public void DeleteProduct_Should_Deactivate_Product()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product(productId, "Produit à supprimer", 10.99m, true);
            var repo = new FakeProductRepository(product);
            var service = new ProductService(repo);

            // Act
            service.DeleteProduct(productId);

            // Assert
            var deletedProduct = repo.GetById(productId);
            Assert.NotNull(deletedProduct);
            Assert.False(deletedProduct.IsActive); // Vérifie que c'est un soft delete
            Assert.True(repo.WasSaved);
        }

        [Fact]
        public void ChangeProductPrice_Should_Save_Product_When_Price_Is_Valid()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product(productId, "Test Product", 10m, true);
            var repo = new FakeProductRepository(product);
            var service = new ProductService(repo);

            // Act
            service.ChangeProductPrice(productId, 20m);

            // Assert
            var updatedProduct = repo.GetById(productId);
            Assert.Equal(20m, updatedProduct.Price);
            Assert.True(repo.WasSaved);
        }

        [Fact]
        public void ChangeProductPrice_Should_Throw_When_Product_Inactive()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product(productId, "Produit Inactif", 10m, false);
            var repo = new FakeProductRepository(product);
            var service = new ProductService(repo);

            // Act & Assert
            Assert.Throws<DomaineException>(() => service.ChangeProductPrice(productId, 20m));
        }

        [Fact]
        public void ApplyProductDiscount_Should_Decrease_Price()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product(productId, "Test Product", 100m, true);
            var repo = new FakeProductRepository(product);
            var service = new ProductService(repo);

            // Act
            service.ApplyProductDiscount(productId, 30m);

            // Assert
            var updatedProduct = repo.GetById(productId);
            Assert.Equal(70m, updatedProduct.Price);
            Assert.True(repo.WasSaved);
        }

        [Fact]
        public void SearchProducts_Should_Find_Products_By_Name()
        {
            // Arrange
            var repo = new FakeProductRepository();
            repo.AddProduct(new Product(Guid.NewGuid(), "Ordinateur Portable", 999.99m, true, "PC portable"));
            repo.AddProduct(new Product(Guid.NewGuid(), "Souris USB", 19.99m, true, "Souris filaire"));
            repo.AddProduct(new Product(Guid.NewGuid(), "Clavier Mécanique", 89.99m, true, "Clavier gaming"));

            var service = new ProductService(repo);

            // Act
            var results = service.SearchProducts("ordinateur").ToList();

            // Assert
            Assert.Single(results);
            Assert.Contains("Ordinateur", results[0].Name);
        }

        [Fact]
        public void GetProductCount_Should_Return_Correct_Count()
        {
            // Arrange
            var repo = new FakeProductRepository();
            repo.AddProduct(new Product(Guid.NewGuid(), "Produit 1", 10.99m, true));
            repo.AddProduct(new Product(Guid.NewGuid(), "Produit 2", 20.99m, true));

            var service = new ProductService(repo);

            // Act
            var count = service.GetProductCount();

            // Assert
            Assert.Equal(2, count);
        }
    }
}