using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Exceptions;
using AdvancedDevSample.Domain.Interfaces.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AdvancedDevSample.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Guid CreateProduct(string name, decimal price, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomaineException("Le nom du produit est requis");

            if (price <= 0)
                throw new DomaineException("Le prix doit être positif");

            var product = new Product(
                id: Guid.NewGuid(),
                name: name,
                price: price,
                isActive: true,
                description: description
            );

            _repository.Save(product);
            return product.Id;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Product> GetActiveProducts()
        {
            return _repository.FindActiveProducts();
        }

        public Product GetProduct(Guid productId)
        {
            var product = _repository.GetById(productId);
            if (product == null)
                throw new ApplicationServiceException("Produit non trouvé", HttpStatusCode.NotFound);

            return product;
        }

        public void UpdateProduct(Guid productId, string name, decimal price, string? description)
        {
            var product = GetProduct(productId);

            if (string.IsNullOrWhiteSpace(name))
                throw new DomaineException("Le nom du produit est requis");

            if (price <= 0)
                throw new DomaineException("Le prix doit être positif");

            product.UpdateDetails(name, description);
            product.ChangePrice(price);

            _repository.Save(product);
        }

        public void DeleteProduct(Guid productId)
        {
            var product = GetProduct(productId);
            product.Deactivate();
            _repository.Save(product);
        }

        public void HardDeleteProduct(Guid productId)
        {
            _repository.Delete(productId);
        }

        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAllProducts();

            searchTerm = searchTerm.ToLower();
            return GetAllProducts()
                .Where(p => p.Name.ToLower().Contains(searchTerm) ||
                           (p.Description != null && p.Description.ToLower().Contains(searchTerm)));
        }

        public int GetProductCount()
        {
            return GetAllProducts().Count();
        }

        public bool ProductExists(Guid productId)
        {
            return _repository.GetById(productId) != null;
        }

        public IEnumerable<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
                throw new DomaineException("Fourchette de prix invalide");

            return _repository.FindByPriceRange(minPrice, maxPrice);
        }

        public void ChangeProductPrice(Guid productId, decimal newPrice)
        {
            var product = GetProduct(productId);
            product.ChangePrice(newPrice);
            _repository.Save(product);
        }

        public void ApplyProductDiscount(Guid productId, decimal discount)
        {
            var product = GetProduct(productId);
            product.ApplyDiscount(discount);
            _repository.Save(product);
        }

        public void ActivateProduct(Guid productId)
        {
            var product = GetProduct(productId);
            product.Activate();
            _repository.Save(product);
        }

        public void DeactivateProduct(Guid productId)
        {
            var product = GetProduct(productId);
            product.Deactivate();
            _repository.Save(product);
        }
    }
}