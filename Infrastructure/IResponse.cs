using Infrastructure.Enum;

namespace Infrastructure
{
    public interface IResponse
    {
        ResponseStatus StatusCode { get; set; }
        string ResponseText { get; set; }
    }
    public interface IResponse<T>
    {
        ResponseStatus StatusCode { get; set; }
        string ResponseText { get; set; }
        T Result { get; set; }
    }
    public class Response
    {
        public ResponseStatus StatusCode { get; set; }
        public string ResponseText { get; set; }
    }
    public class Response<T> : IResponse<T>
    {
        public ResponseStatus StatusCode { get; set; }
        public string ResponseText { get; set; }
        public T Result { get; set; }
        public Response()
        {
            StatusCode = ResponseStatus.Failed;
            ResponseText = ResponseStatus.Failed.ToString();
        }
    }
}
