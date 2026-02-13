using AdvancedDevSample.Application.DTOs;
using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Exceptions;
using AdvancedDevSample.Domain.Interfaces.Customers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AdvancedDevSample.Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }


        public Guid CreateCustomer(CreateCustomerRequest request)
        {
            if (_repository.EmailExists(request.Email))
                throw new DomaineException($"Un client avec l'email {request.Email} existe déjà");

            var customer = new Customer(
                id: Guid.NewGuid(),
                firstName: request.FirstName,
                lastName: request.LastName,
                email: request.Email,
                phone: request.Phone,
                address: request.Address,
                isActive: true
            );

            _repository.Save(customer);
            return customer.Id;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Customer> GetActiveCustomers()
        {
            return _repository.GetActiveCustomers();
        }

        public Customer GetCustomer(Guid customerId)
        {
            var customer = _repository.GetById(customerId);
            if (customer == null)
                throw new ApplicationServiceException("Client non trouvé", HttpStatusCode.NotFound);

            return customer;
        }

        public void UpdateCustomer(Guid customerId, UpdateCustomerRequest request)
        {
            var customer = GetCustomer(customerId);

            if (customer.Email != request.Email && _repository.EmailExists(request.Email))
                throw new DomaineException($"Un client avec l'email {request.Email} existe déjà");

            customer.SetName(request.FirstName, request.LastName);
            customer.SetEmail(request.Email);
            customer.SetPhone(request.Phone);
            customer.SetAddress(request.Address);

            _repository.Save(customer);
        }

        public void DeleteCustomer(Guid customerId)
        {
            var customer = GetCustomer(customerId);
            customer.Deactivate();
            _repository.Save(customer);
        }

        public void HardDeleteCustomer(Guid customerId)
        {
            _repository.Delete(customerId);
        }

        public IEnumerable<Customer> SearchCustomers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAllCustomers();

            searchTerm = searchTerm.ToLower();
            return GetAllCustomers()
                .Where(c => c.FirstName.ToLower().Contains(searchTerm) ||
                           c.LastName.ToLower().Contains(searchTerm) ||
                           c.Email.ToLower().Contains(searchTerm) ||
                           (c.Phone != null && c.Phone.Contains(searchTerm)));
        }

        public bool CustomerExists(Guid customerId)
        {
            return _repository.GetById(customerId) != null;
        }

        public int GetCustomerCount()
        {
            return _repository.GetAll().Count();
        }

        public void ActivateCustomer(Guid customerId)
        {
            var customer = GetCustomer(customerId);
            customer.Activate();
            _repository.Save(customer);
        }

        public void DeactivateCustomer(Guid customerId)
        {
            var customer = GetCustomer(customerId);
            customer.Deactivate();
            _repository.Save(customer);
        }
    }
}
