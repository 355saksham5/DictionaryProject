using DictionaryApp.Extension;
using DictionaryApp.Helpers;
using Microsoft.AspNetCore.Http.Extensions;

namespace DictionaryApp.Middlewares
{
    public class RedirectionMiddleware
    {
        private readonly RequestDelegate next;
        public RedirectionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var currentUrl = context.Request.GetDisplayUrl();
            var userToken = context.Request.Cookies[ConstantResources.cookieName];
            var isRequestAllowed = ConstantResources.allowedUrls.Contains(currentUrl) ||  
                (userToken != null && userToken != "");
            if (!isRequestAllowed)
            {
                context.Response.Redirect(ConstantResources.loginUrl);
            }
            else
            {
                await next(context);
            }
            
        }
    }
}
