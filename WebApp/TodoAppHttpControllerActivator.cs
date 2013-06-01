using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Ninject;

namespace WebApp
{
    public class TodoAppHttpControllerActivator : IHttpControllerActivator
    {
        private readonly IKernel _kernel;

        public TodoAppHttpControllerActivator(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return (IHttpController) _kernel.Get(controllerType);
        }
    }
}