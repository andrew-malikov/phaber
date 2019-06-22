namespace Phaber.Infrastructure.Errors {
    public class Error {
        public readonly string Message;

        public Error(string message) {
            Message = message;
        }
    }
}