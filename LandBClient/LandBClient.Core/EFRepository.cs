using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LandBClient.Core;
using System.Data.Entity.Infrastructure;

namespace LandBClient.Core
{
    public class EFRepository:IRepository
    {
        private readonly LandBContext _dbcontext;
        public EFRepository(LandBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        #region User Methods
        public List<Customer> customerlist()
        {
            List<Customer> custlist = new List<Customer>();
            try {                
                custlist = _dbcontext.Customers.ToList();
            }
            catch(Exception ex) { }
            return custlist;
        }
        public Customer GetCustomerById(int id)
        {
            Customer editobj = new Customer();
            try {
                editobj = _dbcontext.Customers.Find(id);
            }
            catch(Exception ex) { }
            return editobj;
        }
        public bool AddCustomer(Customer customer)
        {
            bool result = false;
            try {
                if (customer != null)
                {
                    _dbcontext.Customers.Add(customer);
                    _dbcontext.SaveChanges();
                    result = true;
                }
            }
            catch(Exception ex) { }
            return result;
        }
        public bool SaveCustomer(Customer customer)
        {
            bool result = false;
            try
            {
                _dbcontext.Entry(customer).State = EntityState.Modified;
                _dbcontext.SaveChanges();
                result = true;
            }
            catch(Exception ex) { }
            return result;
        }
        public bool DeleteCustomer(int id,string username)
        {
            bool result = false;
            try
            {
                Customer delobj = new Customer();
                delobj = _dbcontext.Customers.Find(id);
                bool isUpdatedinTable = AddToDeleteTable(delobj,username);
                if (isUpdatedinTable)
                {
                    _dbcontext.Customers.Remove(delobj);
                    _dbcontext.SaveChanges();
                    result = true;
                }
            }
            catch(Exception ex)
            { 
                               
            }
            return result;
        }
        public bool AddToDeleteTable(Customer delobj,string username)
        {
            bool result = false;
            try
            {
                DeletedCustomer addobj = new DeletedCustomer();              
                if (delobj != null)
                {
                    addobj.FirstName = delobj.FirstName;
                    addobj.LastName = delobj.LastName;
                    addobj.EmailID = delobj.EmailID;
                    addobj.PhoneNumber = delobj.PhoneNumber;
                    addobj.Status = delobj.Status;
                    addobj.LeadSource = delobj.LeadSource;
                    addobj.IsVisited = delobj.IsVisited;
                    addobj.DeletedBy = username;

                    _dbcontext.DeletedCustomers.Add(addobj);
                    _dbcontext.SaveChanges();
                    result = true;
                }
            }
            catch(Exception ex) { }
            return result;
        }
        public List<DeletedCustomer> GetDelCustomersList()
        {
            List<DeletedCustomer> delcustlist = new List<DeletedCustomer>();
            try
            {
                delcustlist = _dbcontext.DeletedCustomers.ToList();
            }
            catch (Exception ex) { }
            return delcustlist;
        }
        public bool moveCustomerToVisited(int ID)
        {
            bool result = false;
            Customer obj = new Customer();
            Customer addobj = new Customer();
            obj = _dbcontext.Customers.Find(ID);
            addobj.FirstName = obj.FirstName;
            addobj.LastName = obj.LastName;
            addobj.PhoneNumber = obj.PhoneNumber;
            addobj.EmailID = obj.EmailID;
            addobj.IsVisited = "yes";
            addobj.Status = obj.Status;
            addobj.LeadSource = obj.LeadSource;
            addobj.ContactDate = obj.ContactDate;
            addobj.OtherReason = obj.OtherReason;           
            if (obj != null)
            {
                try {
                    _dbcontext.Customers.Remove(obj);
                    _dbcontext.SaveChanges();
                    result = true;
                }
                catch(Exception ex) {
                    result = false;
                }                
            }
            if (result)
            {
                try {
                    _dbcontext.Customers.Add(addobj);
                    _dbcontext.SaveChanges();
                }
                catch(Exception ex) {
                    result = false;
                }
            }
            return result;
        }
        public List<Customer> Visitedcustomerlist()
        {
            List<Customer> list = new List<Customer>();
            try
            {
                list = _dbcontext.Customers.ToList();
            }
            catch (Exception ex) { }
            return list;
        }
        #endregion User Methods

        #region Admin Methods
        public bool DeleteCustomerByAdmin(int id) {
            bool result = false;
            Customer deladminobj = new Customer();
            try {
                deladminobj = _dbcontext.Customers.Find(id);
                if (deladminobj != null)
                {
                    _dbcontext.Customers.Remove(deladminobj);
                    _dbcontext.SaveChanges();
                    result = true;
                }
            }
            catch(Exception ex) { }
            return result;
        }
        public bool DeleteDelTableRecord(int id)
        {
            bool result = false;
            try
            {
                DeletedCustomer obj = new DeletedCustomer();
                obj = _dbcontext.DeletedCustomers.Find(id);
                _dbcontext.DeletedCustomers.Remove(obj);
                _dbcontext.SaveChanges();
                result = true;
            }
            catch(Exception ex) { }
            return result;
        }
        public List<TUser> GetAllUsers()
        {
            List<TUser> userlist = new List<TUser>();
            try {
                userlist = _dbcontext.TUsers.ToList();
            }
            catch(Exception ex) { }
            return userlist;
        }
        public bool AddUser(TUser addobj)
        {
            bool result = false;
            try
            {
                _dbcontext.TUsers.Add(addobj);
                _dbcontext.SaveChanges();
                result = true;
            }
            catch(Exception ex) { }
            return result;
        }
        public TUser EditUser(int userid)
        {
            TUser editobj = new TUser();
            try {
                editobj = _dbcontext.TUsers.Find(userid);
            }
            catch(Exception ex) { }
            return editobj;
        }
        public bool DeleteUser(int userid) {
            bool result = false;
            TUser delobj = new TUser();
            try
            {
                delobj = _dbcontext.TUsers.Find(userid);
                _dbcontext.TUsers.Remove(delobj);
                _dbcontext.SaveChanges();
                result = true;
            }
            catch(Exception ex){ }
            return result;
        }
        public bool SaveUser(TUser obj)
        {
            bool result = false;
            try {
                if (obj != null)
                {
                    _dbcontext.Entry(obj).State = EntityState.Modified;
                    _dbcontext.SaveChanges();
                    result = true;
                }
            }
            catch(Exception ex) { }
            return result;
        }
        public bool UnDeleteCustomer(int id)
        {
            bool result = false;
            DeletedCustomer undelobj = new DeletedCustomer();
            Customer addobj = new Customer();
            try
            {
                undelobj = _dbcontext.DeletedCustomers.Find(id);
                if (undelobj != null)
                {
                    addobj.FirstName = undelobj.FirstName;
                    addobj.LastName = undelobj.LastName;
                    addobj.PhoneNumber = undelobj.PhoneNumber;
                    addobj.EmailID = undelobj.EmailID;
                    addobj.IsVisited = undelobj.IsVisited;
                    addobj.LeadSource = undelobj.LeadSource;
                    addobj.Status = undelobj.Status;

                    result = AddCustomer(addobj);
                    if (result)
                    {
                        _dbcontext.DeletedCustomers.Remove(undelobj);
                        _dbcontext.SaveChanges();
                    }
                }
            }
            catch (Exception ex) { }
            return result;
        }
        #endregion Admin Methods
    }
}
