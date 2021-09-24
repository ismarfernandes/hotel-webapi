using Hotel.Shared.Enumerators;

namespace Hotel.Shared.Interfaces
{
    /// <summary>
    /// To define a common response in the whole application, this interface was created to serve this proposal
    /// </summary>
    public interface IResponse
    {
        #region Properties
        public object Result { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public ResponseType Type { get; set; }
        #endregion
    }
}
