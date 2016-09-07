using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiWithDelegatingHandlerExample.Common
{
    public class TimerHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var watch = new Stopwatch();

            watch.Start();

            //calling the inner handler
            return base.SendAsync(request, cancellationToken)
                .ContinueWith
                (
                    t =>
                    {
                        // this will be executed after the inner handler executes asynchronously
                        watch.Stop();
                        Trace.WriteLine($"completed in {watch.ElapsedMilliseconds} milliseconds");

                        return t.Result;
                    }
                );
        }
    }
}