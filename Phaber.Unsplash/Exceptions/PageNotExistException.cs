using System;
using System.Runtime.Serialization;

namespace Phaber.Unsplash.Exceptions {
    [System.Serializable]
    public class PageNotExistException : System.Exception {
        public PageNotExistException() { }

        public PageNotExistException(string message) : base(message) { }

        public PageNotExistException(string message, Exception inner) : base(message, inner) { }

        protected PageNotExistException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}