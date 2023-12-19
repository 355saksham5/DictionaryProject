using System.Net;

namespace DictionaryApi.Helpers
{
    public class AnyHttpException : Exception
    {
        public HttpStatusCode statusCode;
        public override string? Message { get; }

        public AnyHttpException(HttpStatusCode statusCode, string message)
        {
            this.statusCode = statusCode;
            this.Message = message;
        }
    }
}
