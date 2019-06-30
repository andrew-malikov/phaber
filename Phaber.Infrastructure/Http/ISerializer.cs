namespace Phaber.Infrastructure.Http {
    public interface ISerializer {
        string Serialize(object item);
        T Deserialize<T>(string serialized);
    }
}