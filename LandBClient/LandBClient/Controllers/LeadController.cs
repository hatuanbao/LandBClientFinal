using LandBClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LandBClient.Extensions;

namespace LandBClient.Controllers
{
    [MyAuthorize(Roles = "user")]//[Authorize]
    public class LeadController : Controller
    {
        private IRepository _repository;
        public LeadController(IRepository repository)
        {
            _repository = repository;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LeadLookUp()
        {
            List<Customer> customerlist = new List<Customer>();
            customerlist = _repository.customerlist();
            customerlist = customerlist.Where(x => x.IsVisited.ToUpper() == "NO").ToList();
            //string ddlleadsource=customerlist.Where(x=>x.LeadSource.ToUpper()=="OTHER").FirstOrDefault().LeadSource;
            return View(customerlist);
        }
        //public JsonResult jqCustomerList(string gridtype,string sidx,string sord,int page,int rows)
        //{
        //    int pageIndex = Convert.ToInt32(page)-1;
        //    int pagesize = rows;
        //    var CustomerResults = _repository.customerlist().Select(
        //        a=>new {
        //            a.CustomerID,
        //            a.FirstName,
        //            a.LastName,
        //            a.PhoneNumber,
        //            a.EmailID,
        //            a.IsVisited,
        //            a.LeadSource,
        //            a.Status
        //    });
        //    if (gridtype == "NewCustomerGrid")
        //    {
        //        CustomerResults = CustomerResults.Where(x => x.IsVisited.ToUpper() == "YES").ToList();
        //    }
        //    else
        //    {
        //        CustomerResults = CustomerResults.Where(x => x.IsVisited.ToUpper() == "NO").ToList();
        //    }                    
        //    int totalRecords = CustomerResults.Count();
        //    var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
        //    if (sord.ToUpper() == "DESC")
        //    {
        //        CustomerResults = CustomerResults.OrderByDescending(x => x.FirstName);
        //        CustomerResults = CustomerResults.Skip(pageIndex * pagesize).Take(pagesize);
        //    }
        //    else
        //    {
        //        CustomerResults = CustomerResults.OrderBy(x => x.FirstName);
        //        CustomerResults = CustomerResults.Skip(pageIndex * pagesize).Take(pagesize);
        //    }
        //    var jsonData =new
        //    {
        //        total=totalPages, //total number of pages
        //        page, //number of current page
        //        records=totalRecords, //total number of records
        //        rows=CustomerResults //Total records data
        //    };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult AddLead()
        {
            ViewData["AddorEdit"] = "Add";
            return View("AddorEditLeadLookUp");
        }
        [HttpPost]
        public bool AddLead(FormCollection form)
        {
            bool result = false;
            Customer addobj = new Customer();
            try {
                if (form != null)
                {
                    addobj.FirstName = Request.Form["FirstName"];
                    addobj.LastName = Request.Form["LastName"];
                    addobj.PhoneNumber = Request.Form["PhoneNumer"];
                    addobj.EmailID = Request.Form["EmailID"];
                    addobj.IsVisited = Request.Form["ddlIsVisited"];
                    addobj.Status = Request.Form["Status"];
                    addobj.LeadSource = Request.Form["ddlLeadSource"];
                    if (addobj.LeadSource == "other")
                    {
                        addobj.OtherReason = Request.Form["hdnOtherLeadSource"];
                    }
                    bool isSaved = _repository.AddCustomer(addobj);
                    if (isSaved)
                        result = true;
                }
            }
            catch(Exception ex) { }
            return result;
        }
        public ActionResult EditLeadLookup(FormCollection form)
        {
            int id = 0;
            string isvisited = null;
            Customer editobj = new Customer();
            if (Request.Form["hdnCustomerID"] != null)
            {
                id = Convert.ToInt32(Request.Form["hdnCustomerID"].ToString());
            }
            if (Request.Form["hdnIsVisited"] != null)
            {
                isvisited = Request.Form["hdnIsVisited"].ToString();
            }
            editobj = _repository.GetCustomerById(id);
            ViewData["EditCustomerData"] = editobj;
            if (isvisited=="YES")
            {
                ViewData["IsVisited"] = "YES";
            }
            ViewData["AddorEdit"] = "Edit";
            return View("AddorEditLeadLookUp");
        }

        public bool SaveLead(FormCollection form)
        {
            bool result = false;
            Customer editobj = new Customer();
            try
            {
                    editobj.CustomerID = Convert.ToInt32(Request.Form["hdnCustomerID"].ToString());
                    editobj.FirstName = Request.Form["FirstName"];
                    editobj.LastName = Request.Form["LastName"];
                    editobj.PhoneNumber = Request.Form["PhoneNumer"];
                    editobj.EmailID = Request.Form["EmailID"];
                    editobj.IsVisited = Request.Form["ddlIsVisited"];
                    editobj.Status = Request.Form["Status"];
                    editobj.LeadSource = Request.Form["ddlLeadSource"];
                    if (editobj.LeadSource == "other")
                    {
                        editobj.OtherReason = Request.Form["hdnOtherLeadSource"];
                    }
                    if (ModelState.IsValid)
                    {
                        bool isSaved = _repository.SaveCustomer(editobj);
                        if (isSaved)
                            result = true;
                    }
                
            }
            catch (Exception ex) { }
            return result;
        }

        public bool DeleteLead(int CustomerID)
        {
            bool result = false;
            string username = null;
            try {
                if (Session["CurrentUser"] != null)
                {
                    TUser user = new TUser();
                    user = Session["CurrentUser"] as TUser;
                    username = user.Name;
                }
                result = _repository.DeleteCustomer(CustomerID, username);
            }
            catch(Exception ex) { }
            return result;
        }
        public ActionResult VisitedLeadLookup()
        {
            List<Customer> Visitedcustomerlist = new List<Customer>();
            Visitedcustomerlist = _repository.Visitedcustomerlist();
            Visitedcustomerlist = Visitedcustomerlist.Where(x => x.IsVisited.ToUpper() == "YES").ToList();
            return View(Visitedcustomerlist);
        }
        public bool moveToVisited(int ID)
        {
            bool result = _repository.moveCustomerToVisited(ID);
            return result;
        }
    }
}