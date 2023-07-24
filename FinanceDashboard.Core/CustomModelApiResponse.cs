using System.Net;
using System.Security.Principal;

namespace FinanceDashboard.Core
{
    public class CustomModelApiResponse<T> where T: class
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string>? Errors { get; set; }
        public T? Result { get; set; }
    }
}
