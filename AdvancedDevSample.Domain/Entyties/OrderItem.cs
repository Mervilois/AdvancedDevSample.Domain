using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Domain.Entyties
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; private set; }
        public decimal Subtotal => Quantity * UnitPrice;

        public OrderItem(Order order, Product product, int quantity)
        {
            Id = Guid.NewGuid();
            Order = order;
            OrderId = order.Id;
            Product = product;
            ProductId = product.Id;
            Quantity = quantity;
            UnitPrice = product.Price;
        }
    }
}
