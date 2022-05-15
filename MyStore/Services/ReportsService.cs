using MyStore.Domain.Entities;
using MyStore.Domain.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{
    public interface IReportsService
    {
        List<CustomerContact> GetContacts();
        List<Customer> GetCustomersWithNoOrders();
    }

    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository reportsRepository;

        public ReportsService(IReportsRepository reportsRepository)
        {
            this.reportsRepository = reportsRepository;
        }

        public List<Customer> GetCustomersWithNoOrders()
        {
            return this.reportsRepository.GetCustomersWithNoOrders();
        }

        public List<CustomerContact> GetContacts()
        {
            var result = this.reportsRepository.GetContacts();
            return result;
        }
    }
}
