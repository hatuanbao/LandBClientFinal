using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LandBClient.Core;
using LandBClient.Extensions;
using LandBClient.Models;

namespace LandBClient.Controllers
{
    [MyAuthorize(Roles ="admin")]//[Authorize]
    public class AdminController : Controller
    {
        private readonly IRepository _repository;
        public AdminController(IRepository repository)
        {
            _repository = repository;
        }
        // GET: Admin
        public ActionResult Index()
        {
            var AdminViewModel = new AdminHomeViewModel(_repository);
            return View(AdminViewModel);
        }
        public bool DeleteCustomer(int CustomerID)
        {
            bool result = _repository.DeleteCustomerByAdmin(CustomerID);
            return result;
        }
        public bool DeleteCustomerFromDelTable(int ID)
        {
            bool result = _repository.DeleteDelTableRecord(ID);
            return result;
        }
        public ActionResult ManageUsers()
        {
            var userlist = _repository.GetAllUsers();
            userlist = userlist.Where(x => x.Role.ToLower() == "user").ToList();
            return View(userlist);
        }
        public ActionResult AddUser()
        {
            ViewData["EditUser"] = "Add";
            return View("AddorEditUser");
        }
        [HttpPost]
        public bool AddUser(FormCollection form)
        {
            TUser addobj = new TUser();
            bool result = false;
            string isEdit = Request.Form["hdnIsEdit"].ToString();
            try {
                if (form != null)
                {
                    addobj.Name = Request.Form["FullName"].ToString();
                    addobj.UserName = Request.Form["UserName"].ToString();
                    addobj.Password = Request.Form["Password"].ToString();
                    addobj.Role = Request.Form["ddlRole"].ToString();
                    addobj.EmailAddress = Request.Form["Email"].ToString();
                    addobj.PhoneNumber = Request.Form["PhoneNumber"].ToString();
                    addobj.Gender = Request.Form["gender"].ToString();
                    if (isEdit == "Add")
                    {
                        addobj.CreatedDate = DateTime.UtcNow;
                    }
                    if (isEdit == "Edit")
                    {
                        addobj.ID = Convert.ToInt32(Request.Form["hdnUserID"]);
                    }
                }
                if (ModelState.IsValid)
                {
                    if (isEdit == "Add")
                    {
                        result = _repository.AddUser(addobj);
                    }
                    else
                    {
                        result = _repository.SaveUser(addobj);
                    }
                }
            }
            catch(Exception ex) { }
            return result;
        }
        public ActionResult EditUser(FormCollection form)
        {
            int userid = Convert.ToInt32(Request.Form["hdnUserid"]);
            TUser editObj = new TUser();
            editObj = _repository.EditUser(userid);
            ViewData["EditUserData"] = editObj;
            ViewData["EditUser"] = "Edit";
            return View("AddorEditUser");
        }
        public bool DeleteUser(int userid)
        {
            bool result = _repository.DeleteUser(userid);
            return result;
        }
        public bool UnDeleteCustomer(int CustomerID)
        {
            bool result = _repository.UnDeleteCustomer(CustomerID);
            return result;
        }
    }
}