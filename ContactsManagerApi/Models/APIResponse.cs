using System.Net;

namespace ContactsManagerApi.Models {
    public class APIResponse {

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }    
        
        public object  Result { get; set; }

        public APIResponse() { }

        public APIResponse(HttpStatusCode statusCode, bool isSuccess, List<string>? errorMessages, object? result)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            ErrorMessages = errorMessages;
            Result = result;
        }
    }

}
