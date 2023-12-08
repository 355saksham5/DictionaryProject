
using Azure.Core;
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

			var token = accessor?.HttpContext?.Request.Cookies["Authorization"];
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
			return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
		}
	}
}
