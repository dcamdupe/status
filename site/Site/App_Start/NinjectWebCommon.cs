[assembly: WebActivator.PreApplicationStartMethod(typeof(Site.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Site.App_Start.NinjectWebCommon), "Stop")]

namespace Site.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Activation.Providers;
    using DataInterfaces.Repositories;
    using Data.Repositories;
    using Data;
    using SiteLogic;
    using Site.Services;
    using log4net;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IStatusRepository>().To<StatusRepository>();
            kernel.Bind<IUserPasswordRepository>().To<UserPasswordRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IAuthenticationService>().To<AuthenticationService>();
            kernel.Bind<IAuthentication>().To<Authentication>();
            kernel.Bind<HttpSessionStateBase>().ToConstructor<HttpSessionStateWrapper>(x => new HttpSessionStateWrapper(HttpContext.Current.Session));
            kernel.Bind<IStatusService>().To<StatusService>();
            kernel.Bind<HttpRequestBase>().ToConstructor<HttpRequestWrapper>(x => new HttpRequestWrapper(HttpContext.Current.Request));
            kernel.Bind<ILog>().ToMethod(x => LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)).InTransientScope();
            kernel.Bind<IApiServices>().To<ApiServices>();
            kernel.Bind<IApiSessionRepository>().To<ApiSessionRepository>();
        }
    }
}
