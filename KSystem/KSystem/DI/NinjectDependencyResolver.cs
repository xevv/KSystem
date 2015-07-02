using KModel.Entity;
using KModel.Repository;
using KModel.Repository.Interface;
using KSystem.Function;
using KSystem.WCF;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KSystem.DI
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<ISensorDryRepository>().To<SensorDryRepository>().WithConstructorArgument("context", new KBaseEntities());
            _kernel.Bind<IDeviceRepository>().To<DeviceRepository>().WithConstructorArgument("context", new KBaseEntities());
            _kernel.Bind<IUserRepository>().To<UserRepository>().WithConstructorArgument("context", new KBaseEntities());
            _kernel.Bind<IReportRepository>().To<ReportRepository>().WithConstructorArgument("context", new KBaseEntities());
            _kernel.Bind<IHouseRepository>().To<HouseRepository>().WithConstructorArgument("context", new KBaseEntities());
            _kernel.Bind<IEmailNotification>().To<EMailNotification>().WithConstructorArgument("settings", new EmailSettings());
        }
    }
}