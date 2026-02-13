using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Infrastructure.Repositories
{
    public class InMemorySupplierRepository : ISupplierRepository
    {
        private readonly Dictionary<Guid, Supplier> _suppliers = new();

        public Supplier? GetById(Guid id)
        {
            _suppliers.TryGetValue(id, out var supplier);
            return supplier;
        }

        public IEnumerable<Supplier> GetAll() => _suppliers.Values.ToList();

        public void Save(Supplier supplier) => _suppliers[supplier.Id] = supplier;

        public void Delete(Guid id)
        {
            var supplier = GetById(id);
            if (supplier != null)
            {
                supplier.Deactivate();
                Save(supplier);
            }
        }

        public bool EmailExists(string email)
        {
            return _suppliers.Values.Any(s =>
                s.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        // Pour les tests
        public void Seed()
        {
            if (!_suppliers.Any())
            {
                var s1 = new Supplier("TechDistrib", "Pierre Martin", "contact@techdistrib.fr", "0123456789");
                var s2 = new Supplier("OfficeShop", "Marie Dubois", "contact@officeshop.fr", "0987654321");
                _suppliers[s1.Id] = s1;
                _suppliers[s2.Id] = s2;
            }
        }
    }
}
