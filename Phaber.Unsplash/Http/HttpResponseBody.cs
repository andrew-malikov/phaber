using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Phaber.Unsplash.Http {
    public class HttpResponseBody {
        private readonly HttpContent _content;

        public async Task<string> AsStringAsync() => await _content.ReadAsStringAsync();
        public async Task<Stream> AsStream() => await _content.ReadAsStreamAsync();
        public async Task<byte[]> AsByteArray() => await _content.ReadAsByteArrayAsync();

        public HttpResponseBody(HttpContent content) {
            _content = content;
        }
    }
}