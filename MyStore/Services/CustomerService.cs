using AutoMapper;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{
    public interface ICustomerService
    {
        CustomerModel AddCustomer(CustomerModel newCustomer);
        bool DeleteCustomer(int id);
        bool Exists(int id);
        IEnumerable<CustomerModel> GetAllCustomers();
        CustomerModel GetCustomerById(int id);
        void UpdateCustomer(CustomerModel model);
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public IEnumerable<CustomerModel> GetAllCustomers()
        {
            var allCustomers = customerRepository.GetAll().ToList();

            var customerModels = mapper.Map<IEnumerable<CustomerModel>>(allCustomers);

            return customerModels;
        }

        public CustomerModel GetCustomerById(int id)
        {
            var customer = customerRepository.GetById(id);

            var customerModel = mapper.Map<CustomerModel>(customer);
            return customerModel;
        }

        public CustomerModel AddCustomer(CustomerModel newCustomer)
        {
            Customer customerToAdd = mapper.Map<Customer>(newCustomer);

            var addedCustomer = customerRepository.Add(customerToAdd);

            newCustomer = mapper.Map<CustomerModel>(addedCustomer);

            return newCustomer;
        }

        public void UpdateCustomer(CustomerModel model)
        {
            var customerToUpdate = mapper.Map<Customer>(model);
            customerRepository.Update(customerToUpdate);
        }

        public bool Exists(int id)
        {
            return customerRepository.Exists(id);
        }

        public bool DeleteCustomer(int id)
        {
            Customer customerToBeDeleted = customerRepository.GetById(id);

            return customerRepository.Delete(customerToBeDeleted);
        }
    }
}
