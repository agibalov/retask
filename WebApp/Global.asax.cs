using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation.Mvc;
using Ninject;
using Service;
using Service.Infrastructure.Mail.Configuration;
using Service.TransactionScripts.BL;
using WebApp.Configuration;

namespace WebApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FluentValidationModelValidatorProvider.Configure();

            var kernel = new StandardKernel();

            var shouldEnableNotifications = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableNotifications"]);
            if (shouldEnableNotifications)
            {
                var rootUrl = ConfigurationManager.AppSettings["RootUrl"];
                var signUpConfirmationUrlTemplate = ConfigurationManager.AppSettings["SignUpConfirmationUrlTemplate"];
                var resetPasswordUrlTemplate = ConfigurationManager.AppSettings["ResetPasswordUrlTemplate"];
                kernel.Load(new MailingNotifiersModule(
                    rootUrl, 
                    signUpConfirmationUrlTemplate,
                    resetPasswordUrlTemplate));
            }
            else
            {
                kernel.Load(new NullNotifiersModule());
            }

            var mailSettingsConfigurationSection = (MailSettingsConfigurationSection) ConfigurationManager.GetSection("mailSettings");
            kernel.Load(new MailModule(mailSettingsConfigurationSection));
            
            kernel.Bind<ITimeProvider>()
                .To<UtcNowTimeProvider>()
                .InSingletonScope();

            var sessionTtlSeconds = Convert.ToInt32(ConfigurationManager.AppSettings["SessionTtlSeconds"]);
            kernel.Bind<AuthenticationService>()
                .ToSelf()
                .InSingletonScope()
                .WithConstructorArgument("sessionTtlSeconds", sessionTtlSeconds);

            kernel.Bind<RetaskService>().ToSelf().InSingletonScope();

            var controllerFactory = new TodoAppControllerFactory(kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            var controllerActivator = new TodoAppHttpControllerActivator(kernel);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), controllerActivator);
        }
    }
}