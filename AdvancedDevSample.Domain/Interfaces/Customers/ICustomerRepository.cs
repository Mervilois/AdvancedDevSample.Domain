using AdvancedDevSample.Domain.Entyties;
using System;
using System.Collections.Generic;

namespace AdvancedDevSample.Domain.Interfaces.Customers
{
    public interface ICustomerRepository
    {
        Customer? GetById(Guid id);
        IEnumerable<Customer> GetAll();
        IEnumerable<Customer> GetActiveCustomers();
        IEnumerable<Customer> FindByEmail(string email);
        void Save(Customer customer);
        void Delete(Guid id); // Soft delete
        bool Exists(Guid id);
        bool EmailExists(string email);
    }
}