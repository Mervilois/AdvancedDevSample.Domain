using AdvancedDevSample.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Domain.Entyties
{
    public class Supplier
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string ContactName { get; private set; }
        public string Email { get; private set; }
        public string? Phone { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Constructeur
        public Supplier(string name, string contactName, string email, string? phone = null)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetContactName(contactName);
            SetEmail(email);
            Phone = phone;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        // Règles métier simples
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomaineException("Le nom du fournisseur est requis");
            Name = name;
        }

        public void SetContactName(string contactName)
        {
            if (string.IsNullOrWhiteSpace(contactName))
                throw new DomaineException("Le nom du contact est requis");
            ContactName = contactName;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomaineException("L'email est requis");
            if (!email.Contains('@'))
                throw new DomaineException("Email invalide");
            Email = email;
        }

        public void SetPhone(string? phone)
        {
            Phone = phone;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}