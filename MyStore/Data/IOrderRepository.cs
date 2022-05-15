using MyStore.Domain.Entities;
using MyStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface IOrderRepository
    {
        Order Add(Order newOrder);
        bool DeleteOrder(Order order);
        bool Exists(int id);
        IQueryable<Order> GetAll(string? city, List<string>? country, Shippers shippers);
        Order GetById(int id);
        void UpdateOrder(Order updatedOrder);
    }
}
