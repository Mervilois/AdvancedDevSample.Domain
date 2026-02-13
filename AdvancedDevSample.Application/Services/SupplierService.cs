using AdvancedDevSample.Application.DTOs;
using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Exceptions;
using AdvancedDevSample.Domain.Interfaces.Suppliers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdvancedDevSample.Application.Services
{
    public class SupplierService
    {
        private readonly ISupplierRepository _repository;

        public SupplierService(ISupplierRepository repository)
        {
            _repository = repository;
        }

        // CREATE
        public Guid CreateSupplier(CreateSupplierRequest request)
        {
            if (_repository.EmailExists(request.Email))
                throw new DomaineException($"Email déjà utilisé");

            var supplier = new Supplier(
                request.Name,
                request.ContactName,
                request.Email,
                request.Phone
            );

            _repository.Save(supplier);
            return supplier.Id;
        }

        // READ ALL
        public IEnumerable<Supplier> GetAllSuppliers() => _repository.GetAll();

        // READ BY ID
        public Supplier GetSupplier(Guid id)
        {
            var supplier = _repository.GetById(id);
            if (supplier == null)
                throw new Exception("Fournisseur non trouvé");
            return supplier;
        }

        // UPDATE
        public void UpdateSupplier(Guid id, UpdateSupplierRequest request)
        {
            var supplier = GetSupplier(id);

            if (supplier.Email != request.Email && _repository.EmailExists(request.Email))
                throw new DomaineException($"Email déjà utilisé");

            supplier.SetName(request.Name);
            supplier.SetContactName(request.ContactName);
            supplier.SetEmail(request.Email);
            supplier.SetPhone(request.Phone);

            _repository.Save(supplier);
        }

        // DELETE (soft delete) 
        public void DeleteSupplier(Guid id)
        {
            var supplier = GetSupplier(id);
            supplier.Deactivate();
            _repository.Save(supplier);
        }

        // SEARCH
        public IEnumerable<Supplier> SearchSuppliers(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return GetAllSuppliers();

            term = term.ToLower();
            return GetAllSuppliers().Where(s =>
                s.Name.ToLower().Contains(term) ||
                s.ContactName.ToLower().Contains(term));
        }
    }
}
