using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.CrossCutting.IoC
{
    public class DependencyManagerContext
    {
        #region Constructors
        public DependencyManagerContext(IServiceCollection services, IConfiguration configuration)
        {
            Services = services;
            Configuration = configuration;
        }
        #endregion

        #region Properties
        public IServiceCollection Services { get; }
        public IConfiguration Configuration { get; }
        #endregion
    }
}
