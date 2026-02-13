using AdvancedDevSample.Domain.Entyties;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Domain.Interfaces.Products
{
    public interface IProductRepository
    {
        Product GetById(Guid id);
        IEnumerable<Product> GetAll();
        void Save(Product product);
        void Delete(Guid id);

        IEnumerable<Product> FindActiveProducts();
        IEnumerable<Product> FindByPriceRange(decimal minPrice, decimal maxPrice);
    }

    public interface IProductRepositoryAsync
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        void SaveAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
