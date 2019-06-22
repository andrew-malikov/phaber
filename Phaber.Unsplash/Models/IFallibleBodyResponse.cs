namespace Phaber.Unsplash.Models {
    public interface IFallibleBodyResponse<out TV> : IFallibleResponse {
        TV Retrieve();
    }
}