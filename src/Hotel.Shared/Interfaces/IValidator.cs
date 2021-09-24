using System.Threading.Tasks;

namespace Hotel.Shared.Interfaces
{
    /// <summary>
    /// To define an interface to serve the whole application, like a common way to invoke validations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T> where T : class
    {
        /// <summary>
        /// Call a validation to the target input, running rules defines in the specific validator that implements the interface
        /// </summary>
        /// <param name="input">Target input tha will be validated</param>
        /// <returns></returns>
        Task<IResponse> ValidationAsync(T input);
    }
}
