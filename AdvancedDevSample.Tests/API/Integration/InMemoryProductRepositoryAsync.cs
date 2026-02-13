using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedDevSample.Tests.API.Integration
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly Dictionary<Guid, Product> _store = new();

        public Product GetById(Guid id)
        {
            _store.TryGetValue(id, out var product);
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return _store.Values.ToList();
        }

        public void Save(Product product)
        {
            _store[product.Id] = product;
        }

        public void Delete(Guid id)
        {
            _store.Remove(id);
        }

        public IEnumerable<Product> FindActiveProducts()
        {
            return _store.Values.Where(p => p.IsActive).ToList();
        }

        public IEnumerable<Product> FindByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _store.Values.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
        }

        // Méthodes utilitaires pour les tests
        public void Seed(Product product)
        {
            _store[product.Id] = product;
        }

        public void Clear()
        {
            _store.Clear();
        }

        public int Count()
        {
            return _store.Count;
        }

        public bool Exists(Guid id)
        {
            return _store.ContainsKey(id);
        }
    }
}