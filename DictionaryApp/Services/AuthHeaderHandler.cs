
using Azure.Core;
using DictionaryApp.Helpers;
using System.Net.Http.Headers;

namespace DictionaryApp.Services
{
	class AuthHeaderHandler : DelegatingHandler
	{
		private readonly IHttpContextAccessor accessor;
		public AuthHeaderHandler(IHttpContextAccessor accessor)
		{
			this.accessor = accessor;
		}
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{

			var token = accessor.HttpContext?.Request.Cookies[ConstantResources.cookieName];
			request.Headers.Authorization = new AuthenticationHeaderValue(ConstantResources.cookieIdentifier, token);
			return await base.SendAsync(request, cancellationToken);
		}
	}
}
