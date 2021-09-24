namespace Hotel.CrossCutting.IoC
{
    /// <summary>
    /// Responsible for loading the dependencies configured in the profile
    /// </summary>
    public abstract class DependencyProfile
    {
        public abstract void Load(DependencyManagerContext builder);
    }
}
