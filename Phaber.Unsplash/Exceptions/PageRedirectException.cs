using System;
using System.Runtime.Serialization;

namespace Phaber.Unsplash.Exceptions {
    [System.Serializable]
    public class PageRedirectException : System.Exception {
        public readonly Uri PageLink;

        public PageRedirectException(Uri pageLink) {
            PageLink = pageLink;
        }

        public PageRedirectException(Uri pageLink, string message) : base(message) {
            PageLink = pageLink;
        }

        public PageRedirectException(
            Uri pageLink, string message, Exception inner
        ) : base(message, inner) {
            PageLink = pageLink;
        }

        protected PageRedirectException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}