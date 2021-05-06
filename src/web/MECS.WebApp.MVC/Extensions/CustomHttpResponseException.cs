using System;
using System.Net;

namespace MECS.WebApp.MVC.Extensions
{
    public class CustomHttpResponseException : Exception
    {
        public HttpStatusCode StatusCode;
        public CustomHttpResponseException() { }
        public CustomHttpResponseException(string message, Exception innerException)
            : base(message, innerException) { }
        public CustomHttpResponseException(HttpStatusCode httpStatusCode)
        {
            StatusCode = httpStatusCode;
        }
    }
}
