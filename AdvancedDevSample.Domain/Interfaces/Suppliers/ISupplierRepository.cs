using System;
using AdvancedDevSample.Domain.Entyties;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Domain.Interfaces.Suppliers
{
    public interface ISupplierRepository
    {
        Supplier? GetById(Guid id);
        IEnumerable<Supplier> GetAll();
        void Save(Supplier supplier);
        void Delete(Guid id);
        bool EmailExists(string email);
    }
}
