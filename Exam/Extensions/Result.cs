using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace IoC
{
    public class Result : IActionResult
    {
        #region Properties, Private methods

        private ResponseBody Body { get; set; }
        private HttpStatusCode StatusCode { get; set; }
        private IDictionary<string, string> Headers { get; set; }

        protected Result(HttpStatusCode statusCode, ResponseBody body, params KeyValuePair<string, string>[] headers)
        {
            StatusCode = statusCode;
            Body = body;
            Headers = new Dictionary<string, string>(headers);
        }

        #endregion

        #region Successful responses (200–299)
        public static Result Ok<T>(T Data, bool? Res = null) => new(HttpStatusCode.OK, new ResponseBody<T>
        {
            Data = (T)(Data is string ? new object() : Data),
            Message = Data is string _msg ? _msg : "عملیات با موفقیت انجام شد",
            Status = Res ?? Data is not string
        });
        #endregion

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(Body)
            {
                StatusCode = (int?)StatusCode
            };

            foreach (var header in Headers)
                context.HttpContext.Response.Headers.TryAdd(header.Key, header.Value);

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
