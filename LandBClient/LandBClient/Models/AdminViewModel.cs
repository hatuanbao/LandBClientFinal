using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LandBClient.Core;

namespace LandBClient.Models
{
    public class AdminHomeViewModel
    {
        public AdminHomeViewModel(IRepository _repository)
        {
            CustomerData = _repository.customerlist();
            DeletedCustomerData = _repository.GetDelCustomersList();
        }
        public List<Customer> CustomerData { get; set; }
        public List<DeletedCustomer> DeletedCustomerData { get; set; }
    }
}