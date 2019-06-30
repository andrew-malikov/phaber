namespace Phaber.Unsplash.Errors {
    public interface IError {
        string Message { get; }
        string DebugMessage { get; }
    }
}