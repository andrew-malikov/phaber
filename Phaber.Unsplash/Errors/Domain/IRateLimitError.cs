namespace Phaber.Unsplash.Errors.Domain {
    public interface IRateLimitError : IError, IValuableError<RateLimit> { }
}