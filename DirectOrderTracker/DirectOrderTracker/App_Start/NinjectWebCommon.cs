[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DirectOrderTracker.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(DirectOrderTracker.App_Start.NinjectWebCommon), "Stop")]

namespace DirectOrderTracker.App_Start
{
    using DirectOrderTracker.Repositories;
    using DirectOrderTracker.Services;
    using global::Ninject.Web.Common;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using System;
    using System.Web;

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
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                kernel.Bind<IDirectRepository>().To<DirectRepository>();
                kernel.Bind<IInboundRepository>().To<InboundRepository>();
                kernel.Bind<IOutboundRepository>().To<OutboundRepository>();
                kernel.Bind<ISaleService>().To<SalesService>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }        
    }
}
