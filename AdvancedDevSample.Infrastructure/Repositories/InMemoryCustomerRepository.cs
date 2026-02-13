using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Infrastructure.Repositories
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private readonly Dictionary<Guid, Customer> _customers = new();

        public Customer? GetById(Guid id)
        {
            _customers.TryGetValue(id, out var customer);
            return customer;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customers.Values.OrderBy(c => c.LastName).ThenBy(c => c.FirstName);
        }

        public IEnumerable<Customer> GetActiveCustomers()
        {
            return _customers.Values.Where(c => c.IsActive)
                                    .OrderBy(c => c.LastName)
                                    .ThenBy(c => c.FirstName);
        }

        public IEnumerable<Customer> FindByEmail(string email)
        {
            return _customers.Values.Where(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public void Save(Customer customer)
        {
            _customers[customer.Id] = customer;
        }

        public void Delete(Guid id)
        {
            var customer = GetById(id);
            if (customer != null)
            {
                customer.Deactivate();
                Save(customer);
            }
        }

        public bool Exists(Guid id)
        {
            return _customers.ContainsKey(id);
        }

        public bool EmailExists(string email)
        {
            return _customers.Values.Any(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public void Seed()
        {
            if (!_customers.Any())
            {
                var c1 = new Customer("Jean", "Dupont", "jean@email.com", "0123456789");
                var c2 = new Customer("Marie", "Martin", "marie@email.com", "0987654321");
                _customers[c1.Id] = c1;
                _customers[c2.Id] = c2;
            }
        }

        // Méthode utilitaire pour les tests
        public void SeedData()
        {
            if (!_customers.Any())
            {
                _customers[Guid.NewGuid()] = new Customer(
                    id: Guid.NewGuid(),
                    firstName: "Jean",
                    lastName: "Dupont",
                    email: "jean.dupont@email.com",
                    phone: "0123456789",
                    address: "1 rue de Paris, 75001 Paris"
                );

                _customers[Guid.NewGuid()] = new Customer(
                    id: Guid.NewGuid(),
                    firstName: "Marie",
                    lastName: "Martin",
                    email: "marie.martin@email.com",
                    phone: "0987654321",
                    address: "2 avenue des Fleurs, 69001 Lyon"
                );
            }
        }

        public void Clear()
        {
            _customers.Clear();
        }

        public int Count()
        {
            return _customers.Count;
        }
    }
}
