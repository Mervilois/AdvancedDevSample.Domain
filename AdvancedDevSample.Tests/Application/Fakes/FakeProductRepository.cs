using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedDevSample.Tests.Application.Fakes
{
    public class FakeProductRepository : IProductRepository
    {
        public bool WasSaved { get; private set; }
        private readonly Dictionary<Guid, Product> _products = new();

        public FakeProductRepository() { }

        public FakeProductRepository(Product product)
        {
            if (product != null)
                _products[product.Id] = product;
        }

        // Implémentation de toutes les méthodes de l'interface
        public IEnumerable<Product> GetAll()
        {
            return _products.Values.ToList();
        }

        public Product GetById(Guid id)
        {
            return _products.TryGetValue(id, out var product) ? product : null;
        }

        public void Save(Product product)
        {
            WasSaved = true;
            _products[product.Id] = product;
        }

        public void Delete(Guid id)
        {
            if (_products.ContainsKey(id))
            {
                var product = _products[id];
                product.Deactivate(); // Soft delete
                Save(product);
            }
        }

        public IEnumerable<Product> FindActiveProducts()
        {
            return _products.Values.Where(p => p.IsActive).ToList();
        }

        public IEnumerable<Product> FindByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _products.Values.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
        }

        // Méthodes utilitaires pour les tests
        public void AddProduct(Product product)
        {
            _products[product.Id] = product;
        }

        public void AddProducts(params Product[] products)
        {
            foreach (var product in products)
            {
                _products[product.Id] = product;
            }
        }

        public void Clear()
        {
            _products.Clear();
            WasSaved = false;
        }

        public int Count()
        {
            return _products.Count;
        }

        public bool Exists(Guid id)
        {
            return _products.ContainsKey(id);
        }
    }
}