using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using LandBClient.Core;
using LandBClient.Providers;

namespace LandBClient.App_Start
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IRepository, EFRepository>();
            container.RegisterType<IAuthProvider, AuthProvider>();
            //container.RegisterType<ILogger, FakeLogger>();
            //MvcUnityContainer.Container = container;
            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}