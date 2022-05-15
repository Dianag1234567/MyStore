using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface ISupplierRepository
    {
        Supplier Add(Supplier newSupplier);
        bool Delete(Supplier supplierToDelete);
        bool Exists(int id);
        IEnumerable<Supplier> GetAll();
        Supplier GetById(int id);
        void Update(Supplier supplierToUpdate);
    }
    public class SupplierRepository : ISupplierRepository
    {
        private readonly StoreContext storecontext;
        public SupplierRepository(StoreContext storecontext)
        {
            this.storecontext = storecontext;
        }

        public IEnumerable<Supplier> GetAll()
        {
            return storecontext.Suppliers.ToList();
        }

        public Supplier GetById(int id)
        {
            return storecontext.Suppliers.Find(id);
        }

        public Supplier Add(Supplier newSupplier)
        {
            var addedSupplier = storecontext.Add(newSupplier);
            storecontext.SaveChanges();

            return addedSupplier.Entity;
        }

        public void Update(Supplier supplierToUpdate)
        {
            storecontext.Update(supplierToUpdate);
            storecontext.SaveChanges();
        }

        public bool Exists(int id)
        {
            var exists = storecontext.Suppliers.Count(x => x.Supplierid == id);
            return exists == 1;
        }
        public bool Delete(Supplier supplierToDelete)
        {
            var deleted = storecontext.Suppliers.Remove(supplierToDelete);

            storecontext.SaveChanges();

            return deleted != null;
        }
    }
}
