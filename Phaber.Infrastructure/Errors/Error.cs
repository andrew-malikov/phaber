using Phaber.Unsplash.Errors;

namespace Phaber.Infrastructure.Errors {
    public class Error : IError {
        public string Message { get; private set; }
        public string DebugMessage { get; private set; }

        public Error(string debugMessage, string message) {
            DebugMessage = debugMessage;
            Message = message;
        }
    }
}