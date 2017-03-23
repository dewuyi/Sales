namespace Moonpig.Services.Orders
{
    using System.Web.Http;
    using Microsoft.Practices.Unity;
    
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
			var container = new UnityContainer();
            // Register other types or instances.
            configuration.DependencyResolver = new UnityResolver(container);
        }
    }
}