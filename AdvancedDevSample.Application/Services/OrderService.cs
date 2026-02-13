using AdvancedDevSample.Application.DTOs;
using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces.Customers;
using AdvancedDevSample.Domain.Interfaces.Oders;
using AdvancedDevSample.Domain.Interfaces.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        // CREATE
        public Guid CreateOrder(CreateOrderRequest request)
        {
            // 1. Récupérer le client
            var customer = _customerRepository.GetById(request.CustomerId);
            if (customer == null)
                throw new Exception("Client non trouvé");

            // 2. Créer la commande
            var order = new Order(customer);

            // 3. Ajouter les produits
            foreach (var item in request.Items)
            {
                var product = _productRepository.GetById(item.ProductId);
                if (product == null)
                    throw new Exception($"Produit {item.ProductId} non trouvé");

                order.AddItem(product, item.Quantity);
            }

            // 4. Sauvegarder
            _orderRepository.Save(order);
            return order.Id;
        }

        // READ
        public Order GetOrder(Guid id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
                throw new Exception("Commande non trouvée");
            return order;
        }

        public IEnumerable<Order> GetAllOrders() => _orderRepository.GetAll();

        public IEnumerable<Order> GetCustomerOrders(Guid customerId)
            => _orderRepository.GetByCustomer(customerId);

        // UPDATE STATUS
        public void ConfirmOrder(Guid id)
        {
            var order = GetOrder(id);
            order.Confirm();
            _orderRepository.Save(order);
        }

        public void CancelOrder(Guid id)
        {
            var order = GetOrder(id);
            order.Cancel();
            _orderRepository.Save(order);
        }
    }
}
