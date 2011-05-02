using System.Web.Mvc;
using StructureMap;
using TryCatchFail.CodeStock2011.FailTracker.Web.Infrastructure.DependencyResolution;

[assembly: WebActivator.PreApplicationStartMethod(typeof(TryCatchFail.CodeStock2011.FailTracker.Web.App_Start.StructuremapMvc), "Start")]

namespace TryCatchFail.CodeStock2011.FailTracker.Web.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }
}