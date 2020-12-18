using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace FirstTicketDemo.Infrastructure.Attribute
{
    public class RequestRateLimitAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
        
        public int Seconds { get; set; }
        
        private static MemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions());

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ipAddress = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress;

            var memoryKey = $"{Name}-{ipAddress}";
            
            if (!Cache.TryGetValue(memoryKey, out bool result))
            {
                var entryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(Seconds));

                Cache.Set(memoryKey, true, entryOptions);
            }
            else
            {
                context.Result = new ContentResult()
                {
                    Content = $"Request too many times in {Seconds} seconds"
                };

                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.TooManyRequests;
            }
        }
    }
}
