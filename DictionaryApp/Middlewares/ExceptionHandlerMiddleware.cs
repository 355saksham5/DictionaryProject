﻿using DictionaryApi.Models;
using DictionaryApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Net;
using System.Reflection.Metadata;
using System.Text.Json;

namespace DictionaryApp.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private RequestDelegate next { get; set; }
		private IHostEnvironment env { get; set; }
		private ILogger<ExceptionHandlerMiddleware> logger { get; set; }
		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger
			, IHostEnvironment env)
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
				HttpStatusCode statusCode = (exception as ApiException)?.StatusCode ?? HttpStatusCode.InternalServerError;
				if(statusCode == HttpStatusCode.NotFound)
				{
					context.Response.Redirect(ConstantResources.wordNotExistPageUrl);
				}
				else if (statusCode == HttpStatusCode.InternalServerError)
				{
					context.Response.Redirect(ConstantResources.errorPageUrl);
				}
                else
				{
					context.Response.Redirect($"{ConstantResources.errorPageUrl}/{(int)statusCode}");

                }
				
			}
		}
	}
}
