using System.Web.Mvc;
using StructureMap;
using FailTracker.Web.Infrastructure.DependencyResolution;

[assembly: WebActivator.PreApplicationStartMethod(typeof(FailTracker.Web.App_Start.StructuremapMvc), "Start")]

namespace FailTracker.Web.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }
}