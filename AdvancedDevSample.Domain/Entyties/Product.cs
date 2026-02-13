using AdvancedDevSample.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;


namespace AdvancedDevSample.Domain.Entyties
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public bool IsActive { get; private set; }
        public string? Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdateAt { get; private set; }

        public Product() : this(Guid.NewGuid(), 0, true) { }

        // Constructeur original (sans name)
        public Product(Guid id, decimal price, bool isActive)
        {
            Id = id;
            Price = price;
            IsActive = isActive;
            Name = string.Empty; // Valeur par défaut
        }

        // NOUVEAU : Constructeur avec name
        public Product(Guid id, string name, decimal price, bool isActive, string? description = null)
        {
            Id = id;
            Name = name ?? throw new DomaineException("Le nom du produit est requis");
            Price = price;
            IsActive = isActive;
            Description = description;
        }


        public void ChangePrice (decimal newPrice)
        {
            if (newPrice <= 0)
                throw new DomaineException("Le prix doit etre positif");

            if (!IsActive)
                throw new DomaineException("Produit Incatif");

            Price = newPrice;
            UpdateAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomaineException("Le nom du produit est requis");

            Name = name;
            Description = description;
            UpdateAt = DateTime.UtcNow;
        }

        public void ApplyDiscount(decimal discount)
        {
                ChangePrice(Price -  discount);
        }

        public void Activate()
        {
            IsActive = true;
            UpdateAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
