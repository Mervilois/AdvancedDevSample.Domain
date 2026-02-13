using AdvancedDevSample.Domain.Enums;
using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Domain.Entyties
{
    public class Order
    {
        public Guid Id { get; private set; }
        public string OrderNumber { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order(Customer customer)
        {
            Id = Guid.NewGuid();
            OrderNumber = "CMD-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
            Customer = customer ?? throw new DomaineException("Client requis");
            CustomerId = customer.Id;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
            Items = new List<OrderItem>();
            TotalAmount = 0;
        }

        public void AddItem(Product product, int quantity)
        {
            if (product == null) throw new DomaineException("Produit requis");
            if (quantity <= 0) throw new DomaineException("Quantité > 0");
            if (Status != OrderStatus.Pending) throw new DomaineException("Commande non modifiable");

            var existing = Items.Find(i => i.ProductId == product.Id);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                Items.Add(new OrderItem(this, product, quantity));
            }

            CalculateTotal();
        }

        public void RemoveItem(Guid productId)
        {
            var item = Items.Find(i => i.ProductId == productId);
            if (item != null)
            {
                Items.Remove(item);
                CalculateTotal();
            }
        }

        private void CalculateTotal()
        {
            TotalAmount = 0;
            foreach (var item in Items)
            {
                TotalAmount += item.Subtotal;
            }
        }

        public void Confirm()
        {
            if (Status != OrderStatus.Pending)
                throw new DomaineException("Commande déjà traitée");
            if (Items.Count == 0)
                throw new DomaineException("Commande vide");

            Status = OrderStatus.Confirmed;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Confirmed)
                throw new DomaineException("Commande déjà confirmée");
            Status = OrderStatus.Cancelled;
        }
    }
}
