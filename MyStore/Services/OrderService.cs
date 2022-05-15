using AutoMapper;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Infrastructure;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{
    public interface IOrderService
    {
        Order Add(Order newOrder);
        bool DeleteOrder(int id);
        bool Exists(int id);
        IEnumerable<Order> GetAll(string? city, List<string>? country, Shippers shippers);
        Order GetById(int id);
        void UpdateOrder(OrderModel updatedOrder);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<Order> GetAll(string? city, List<string>? country, Shippers shippers)
        {
            var allOrders = repository.GetAll(city,country,shippers).ToList();

            return allOrders;
        }

        public Order Add(Order newOrder)
        {
            return repository.Add(newOrder);
        }

        public Order GetById(int id)
        {
            return repository.GetById(id);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public void UpdateOrder(OrderModel updatedOrder)
        {
            var order = mapper.Map<Order>(updatedOrder);

            repository.UpdateOrder(order);
        }

        public bool DeleteOrder(int id)
        {
            var orderToDelete = repository.GetById(id);

            return repository.DeleteOrder(orderToDelete);
        }
    }
}