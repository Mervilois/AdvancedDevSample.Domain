using AdvancedDevSample.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Domain.Entyties
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string? Phone { get; private set; }
        public string? Address { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Customer()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public Customer(string firstName, string lastName, string email, string? phone = null)
        {
            Id = Guid.NewGuid();  
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public Customer(Guid id, string firstName, string lastName, string email,
                       string? phone = null, string? address = null, bool isActive = true)
        {
            Id = id;
            SetName(firstName, lastName);
            SetEmail(email);
            Phone = phone;
            Address = address;
            IsActive = isActive;
            CreatedAt = DateTime.UtcNow;
        }

        // Règles métier
        public void SetName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomaineException("Le prénom est requis");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomaineException("Le nom est requis");

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomaineException("L'email est requis");

            // Validation simple d'email
            if (!email.Contains('@') || !email.Contains('.'))
                throw new DomaineException("Format d'email invalide");

            Email = email.Trim().ToLower();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPhone(string? phone)
        {
            Phone = phone;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetAddress(string? address)
        {
            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public string FullName => $"{FirstName} {LastName}";
    }
}
