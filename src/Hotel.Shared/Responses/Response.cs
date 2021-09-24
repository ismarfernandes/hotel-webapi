using Hotel.Shared.Enumerators;
using Hotel.Shared.Interfaces;

namespace Hotel.Shared.Responses
{
    public class Response : IResponse
    {
        #region Properties
        public object Result { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public ResponseType Type { get; set; }
        #endregion
    }
}
