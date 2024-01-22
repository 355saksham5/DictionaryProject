using DictionaryApi.Helpers;
using DictionaryApi.Models;
using Refit;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;

namespace DictionaryApi.Middlewares
{
	[ExcludeFromCodeCoverage]
	public class ExceptionHandlerMiddleware
	{
		private RequestDelegate next { get; set; }
		private IHostEnvironment env { get; set; }
		private ILogger<ExceptionHandlerMiddleware> logger { get; set; }
		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger
			,IHostEnvironment env)
		{
			this.next = next;
			this.logger = logger;
			this.env = env;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception exception)
			{
				ErrorModel response;
				HttpStatusCode statusCode = ((exception as ApiException)?.StatusCode ?? (exception as AnyHttpException)?.statusCode)
					                                ?? HttpStatusCode.InternalServerError;
			    var exceptionType = exception.GetType();
				if(env.IsDevelopment())
				{
					response = new ErrorModel((int)statusCode, exception.Message, JsonSerializer.Serialize(exception.StackTrace));
				}
				else
				{
					response = new ErrorModel((int)statusCode, exception.Message);
				}
				
				logger.LogError(exception, exception.Message);
				context.Response.StatusCode = (int)statusCode;
				context.Response.ContentType = ConstantResources.exceptionResponseType;
				await context.Response.WriteAsync(JsonSerializer.Serialize(response));
			}
		}
	}
}
