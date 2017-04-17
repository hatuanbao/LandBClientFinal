using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandBClient.Core;

namespace LandBClient.Core
{
    public interface IRepository
    {
        #region User Methods

        List<Customer> customerlist();
        Customer GetCustomerById(int id);
        bool AddCustomer(Customer customer);
        bool SaveCustomer(Customer customer);
        bool DeleteCustomer(int id,string username);
        bool AddToDeleteTable(Customer delobj,string username);
        bool moveCustomerToVisited(int ID);
        List<Customer> Visitedcustomerlist();

        #endregion User Methods

        #region Admin Methods

        List<DeletedCustomer> GetDelCustomersList();
        bool DeleteCustomerByAdmin(int id);
        bool DeleteDelTableRecord(int id);
        List<TUser> GetAllUsers();
        bool AddUser(TUser addobj);
        TUser EditUser(int userid);
        bool DeleteUser(int userid);
        bool SaveUser(TUser obj);
        bool UnDeleteCustomer(int CustomerID);
        #endregion Admin Methods
    }
}
