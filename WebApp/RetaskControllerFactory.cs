using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;

namespace WebApp
{
    public class RetaskControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public RetaskControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }

            return (IController)_kernel.Get(controllerType);
        }
    }
}