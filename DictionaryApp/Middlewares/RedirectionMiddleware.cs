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
            var currentHttpMethod = context.Request.Method;
            var userToken = context.Request.Cookies[ConstantResources.cookieName];
            var isRequestAllowed = ConstantResources.allowedGetUrls.Contains(currentUrl) || currentHttpMethod.CustomEquals("Post") || userToken != null && userToken != "";
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
