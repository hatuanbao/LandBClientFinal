using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandBClient.Core
{
    public class LandBContext : DbContext
    {
        public LandBContext()
            : base("name=LandBDB_TestEntities")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeletedCustomer> DeletedCustomers { get; set; }
        public DbSet<TUser> TUsers { get; set; }
    }
}
