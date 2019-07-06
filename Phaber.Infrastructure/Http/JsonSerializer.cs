using Newtonsoft.Json;

namespace Phaber.Infrastructure.Http {
    public class JsonSerializer : ISerializer {
        public string Serialize(object item) {
            return JsonConvert.SerializeObject(item);
        }

        public T Deserialize<T>(string serialized) {
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}