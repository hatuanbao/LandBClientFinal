using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LandBClient.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Field Required")]
        public string username { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public string password { get; set; }
    }
}