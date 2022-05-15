using Microsoft.EntityFrameworkCore;
using MyStore.Domain.Entities;
using MyStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{ 
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext context;

        public OrderRepository(StoreContext context)
        {
            this.context = context;
        }

        //add filters
        //public IEnumerable<Order> GetAll()
        //{
        //    return this.context.Orders
        //        .Include(x => x.Cust)
        //        .ToList();
        //}
        public IQueryable<Order> GetAll(string? city, List<string>? country, Shippers shippers)
        {
            var query = this.context.Orders
                .Include(x => x.OrderDetails)
                .Select(x=>x);

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(x => x.Shipcity == city);
            }

            query = query.Where(x => x.Shipperid == (int)shippers);


            if (country.Any())
            {
                query = query.Where(x => country.Contains(x.Shipcountry));
            }

            //var pageNumber = 3;
            //var itemsPerPage = 20;

            //query = query.Skip(pageNumber - 1 * itemsPerPage).Take(itemsPerPage);

            return query;
        }

        public Order Add(Order newOrder)
        {
            var savedEntity= context.Orders.Add(newOrder).Entity;
            context.SaveChanges();

            return savedEntity;        

        }

        public Order GetById(int id)
        {
            var order = context.Orders
                .Include(x => x.OrderDetails)
                .FirstOrDefault(x => x.Orderid == id);

            return order;
        }

        public bool Exists(int id)
        {
            var exists = context.Orders.Count(x => x.Orderid == id);
            return exists == 1;
        }

        public void UpdateOrder(Order updatedOrder)
        {
            context.Orders.Update(updatedOrder);
            context.SaveChanges();
        } 

        public bool DeleteOrder(Order order)
        {
            var deleted = context.Orders.Remove(order);
            context.SaveChanges();

            return deleted != null;
        }
    }
}
