using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using LandBClient.Core;

namespace LandBClient.Providers
{
    public class AuthProvider:IAuthProvider
    {
        private readonly LandBContext _dbcontext;
        public AuthProvider(LandBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public bool IsLoggedin
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }
        public TUser LoginCheck(string UserName, string Password)
        {
            TUser CurrentUser = new TUser();
            try {            
                CurrentUser = _dbcontext.TUsers.Where(x => x.UserName == UserName && x.Password == Password).FirstOrDefault();
                //if (CurrentUser.UserName != null)
                //    FormsAuthentication.SetAuthCookie(UserName, false);               
            }
            catch(Exception ex) { }
            return CurrentUser;
        }
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}
