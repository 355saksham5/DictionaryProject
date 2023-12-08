using DictionaryApp.Extension;
using Microsoft.AspNetCore.Http.Extensions;

namespace DictionaryApp.Helpers
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
			var userId = context.Request.Cookies["Authorization"];
			var isRequestAllowed = ConstantResources.allowedGetUrls.Contains(currentUrl) || currentHttpMethod.CustomEquals("Post") || (userId != null && userId!="");
			if (!isRequestAllowed)
			{
				context.Response.Redirect(ConstantResources.loginUrl);
			}

			await next(context);
		}
	}
}
