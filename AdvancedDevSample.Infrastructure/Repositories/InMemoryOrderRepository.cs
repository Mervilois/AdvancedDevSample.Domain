using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces.Oders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Infrastructure.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly Dictionary<Guid, Order> _orders = new();

        public Order? GetById(Guid id)
        {
            _orders.TryGetValue(id, out var order);
            return order;
        }

        public IEnumerable<Order> GetAll()
            => _orders.Values.OrderByDescending(o => o.OrderDate).ToList();

        public void Save(Order order)
            => _orders[order.Id] = order;

        public IEnumerable<Order> GetByCustomer(Guid customerId)
            => _orders.Values.Where(o => o.CustomerId == customerId).ToList();
    }
}
