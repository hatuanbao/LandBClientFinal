using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandBClient.Core;

namespace LandBClient.Providers
{
    public interface IAuthProvider
    {
        bool IsLoggedin { get; }
        TUser LoginCheck(string UserName, string Password);
        void Logout();
    }
}
