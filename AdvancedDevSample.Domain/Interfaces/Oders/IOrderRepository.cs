using AdvancedDevSample.Domain.Entyties;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Domain.Interfaces.Oders
{
    public interface IOrderRepository
    {
        Order? GetById(Guid id);
        IEnumerable<Order> GetAll();
        void Save(Order order);
        IEnumerable<Order> GetByCustomer(Guid customerId);
    }
}
