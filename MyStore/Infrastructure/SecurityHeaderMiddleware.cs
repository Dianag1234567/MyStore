using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Infrastructure
{
    public class SecurityHeaderMiddleware
    {
        private readonly RequestDelegate next;

        public SecurityHeaderMiddleware(RequestDelegate next)
        {
            this.next = next;
        } 

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("We", "AreAwsome");
            await this.next.Invoke(httpContext);
        }
    }
}
