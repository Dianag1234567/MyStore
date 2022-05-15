using Microsoft.EntityFrameworkCore;
using MyStore.Domain.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities
{
    public partial class StoreContext
    {
        public DbSet<CustomerContact> CustomerContact { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerContact>().HasNoKey();
        }
        
    }
}
