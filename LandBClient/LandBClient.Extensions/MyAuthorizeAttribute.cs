using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

namespace LandBClient.Extensions
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    var identity = new GenericIdentity(httpContext.User.Identity.Name);
                    var roles = (ticket.UserData ?? string.Empty).Split('|');
                    httpContext.User = new GenericPrincipal(identity, roles);
                }
            }

            return base.AuthorizeCore(httpContext);
        }
    }
}
