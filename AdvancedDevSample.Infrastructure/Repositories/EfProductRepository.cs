using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedDevSample.Infrastructure.Repositories
{
    public class EfProductRepository : IProductRepository
    {
        // Simulation d'une base de données en mémoire
        private static readonly List<Product> _products = new();
        private static int _nextId = 1;

        public IEnumerable<Product> GetAll()
        {
            return _products.AsEnumerable();
        }

        public Product GetById(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Save(Product product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                _products.Remove(existing);
            }
            _products.Add(product);
        }

        public void Delete(Guid id)
        {
            var product = GetById(id);
            if (product != null)
            {
                product.Deactivate();
                Save(product); 
            }
        }

        // Méthodes de recherche
        public IEnumerable<Product> FindActiveProducts()
        {
            return _products.Where(p => p.IsActive);
        }

        public IEnumerable<Product> FindByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
        }

        public IEnumerable<Product> GetAllActive()
        {
            return _products.Where(p => p.IsActive);
        }

        public IEnumerable<Product> GetAllInactive()
        {
            return _products.Where(p => !p.IsActive);
        }

        public void SeedData()
        {
            if (!_products.Any())
            {
                _products.Add(new Product(
                    id: Guid.NewGuid(),
                    name: "Ordinateur Portable",
                    price: 999.99m,
                    isActive: true,
                    description: "Ordinateur portable 15 pouces, 16GB RAM, 512GB SSD"
                ));

                _products.Add(new Product(
                    id: Guid.NewGuid(),
                    name: "Souris Sans Fil",
                    price: 29.99m,
                    isActive: true,
                    description: "Souris Bluetooth avec batterie longue durée"
                ));

                _products.Add(new Product(
                    id: Guid.NewGuid(),
                    name: "Clavier Mécanique",
                    price: 89.99m,
                    isActive: true,
                    description: "Clavier mécanique RGB rétroéclairé"
                ));

                _products.Add(new Product(
                    id: Guid.NewGuid(),
                    name: "Produit Désactivé",
                    price: 49.99m,
                    isActive: false,
                    description: "Ce produit est désactivé"
                ));
            }
        }

        public void Clear()
        {
            _products.Clear();
        }

        public int Count()
        {
            return _products.Count;
        }

        public bool Exists(Guid id)
        {
            return _products.Any(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Task.FromResult(GetAll());
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(GetById(id));
        }

        public async Task SaveAsync(Product product)
        {
            Save(product);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            Delete(id);
            await Task.CompletedTask;
        }
    }
}