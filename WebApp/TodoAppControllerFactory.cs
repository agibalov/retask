using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using WebApp.Controllers;

namespace WebApp
{
    public class TodoAppControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public TodoAppControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }

            return (IController)_kernel.Get(controllerType);
        }
    }
}