namespace ecommerce.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public RequestLoggingMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public static async Task Invoke(HttpContext httpContext)
        {


        }
    }
}
