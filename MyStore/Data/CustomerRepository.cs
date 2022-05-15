using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface ICustomerRepository
    {
        Customer Add(Customer newCustomer);
        bool Delete(Customer customerToDelete);
        bool Exists(int id);
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        void Update(Customer customerToUpdate);
    }
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StoreContext context;

        public CustomerRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return context.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return context.Customers.Find(id);
        }

        public Customer Add(Customer newCustomer)
        {
            var addedCustomer = context.Customers.Add(newCustomer);

            context.SaveChanges();
            return addedCustomer.Entity;
        }

        public void Update(Customer customerToUpdate)
        {
            context.Customers.Update(customerToUpdate);
            context.SaveChanges();
        }

        public bool Exists(int id)
        {
            var numbOfEntryes = context.Customers.Count(x => x.Custid == id);
            return numbOfEntryes == 1;
        }

        public bool Delete(Customer customerToDelete)
        {
            var deleted = context.Customers.Remove(customerToDelete);
            context.SaveChanges();

            return deleted != null;
        }

    }
}
