using System.Net;

namespace WebAPP.Models
{
    public class HttpResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string HttpMessage { get; set; }
        public string Result { get; set; }
    }
}
