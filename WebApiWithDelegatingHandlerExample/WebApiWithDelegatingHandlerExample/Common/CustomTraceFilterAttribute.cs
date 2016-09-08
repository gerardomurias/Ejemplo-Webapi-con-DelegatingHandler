using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiWithDelegatingHandlerExample.Common
{
    public class CustomTraceFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        public bool AllowMultiple => true;

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            Trace.WriteLine("Trace filter start");

            foreach (var item in actionContext.ActionArguments.Keys)
            {
                Trace.WriteLine($"Traza -- { item }: { actionContext.ActionArguments[item] }");
            }

            var response = await continuation();

            Trace.WriteLine($"Trace filter response: { response }");

            return response;
        }
    }
}