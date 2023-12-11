using DictionaryApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DictionaryApp.Controllers
{
	[AllowAnonymous]
	public class ErrorController : Controller
	{
		private readonly ILogger<ErrorController> logger;

		public ErrorController(ILogger<ErrorController> logger)
		{
			this.logger = logger;
		}

        [Route("[controller]/WordNotFound")]
        public IActionResult WordNotFound(int statusCode)
        {
            return View(nameof(WordNotFound));
        }

        [Route("[controller]/{statusCode}")]
		public IActionResult HttpStatusCodeHandler(int statusCode)
		{
			var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

			switch (statusCode)
			{
                case 404:
                    {
                        ViewBag.ErrorMessage = ConstantResources.notFoundErr;
                        logger.LogWarning($"{statusCode} Error Occured. Path = {statusCodeResult?.OriginalPath}" +
                            $" and QueryString = {statusCodeResult?.OriginalQueryString}");
                        return View("NotFound");
                    }

                case 401:
                    {
                        logger.LogWarning($"{statusCode} Error Occured. Path = {statusCodeResult?.OriginalPath}" +
                        $" and QueryString = {statusCodeResult?.OriginalQueryString}");
                        return RedirectToAction("Login", "Account");
                    }

                default:
                    {
                        ViewBag.ErrorTitle = $"{statusCode} Error Occured.";
                        logger.LogWarning($"{statusCode} Error Occured. Path = {statusCodeResult?.OriginalPath}" +
                            $" and QueryString = {statusCodeResult?.OriginalQueryString}");
                        return View("Error");
                    }
            }
		}

		[Route("[controller]")]
		[AllowAnonymous]
		public IActionResult Error()
		{
			var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

			logger.LogError($"The path {exceptionDetails?.Path} threw an exception " +
				$"{exceptionDetails?.Error}");

			return View("Error");
		}
	}
}
