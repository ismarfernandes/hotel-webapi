using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hotel.CrossCutting.IoC
{
    public sealed class DependencyManager
    {
        #region Constructors
        public DependencyManager(DependencyManagerContext context)
        {
            Context = context;
        }
        #endregion

        #region Properties
        public DependencyManagerContext Context { get; }
        #endregion

        #region Attributes
        private List<DependencyProfile> registered = new List<DependencyProfile>();
        #endregion

        #region Public Methods
        public void BuildContainer()
        {
            RegisterProfiles();
            LoadProfiles();
        }
        #endregion

        #region Methods
        private IEnumerable<DependencyProfile> GetProfiles()
        {
            return Assembly.GetAssembly(typeof(DependencyManager))
                .GetExportedTypes()
                .Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(DependencyProfile)))
                .Select(type => (DependencyProfile)Activator.CreateInstance(type))
                .OrderBy(profile => profile.GetType().FullName);
        }

        private void RegisterProfiles()
        {
            var profiles = GetProfiles();
            registered.AddRange(profiles);
        }

        private void LoadProfiles()
        {
            foreach (var profile in registered)
            {
                profile.Load(Context);
            }
        }
        #endregion
    }
}
