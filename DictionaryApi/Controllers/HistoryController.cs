using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Helpers;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;
using DictionaryApi.Models.UserCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace DictionaryApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
	[Route("api/[controller]")]
	[ApiVersion(ConstantResources.apiVersion)]
	public class HistoryController : ControllerBase
	{
		private readonly IUserCacheService userCache;
		public HistoryController(IUserCacheService userCache)
		{
			this.userCache = userCache;
		}

		[HttpGet()]
		public async Task<IEnumerable<CachedWord>> History()
		{
			var history = await userCache.GetCacheAsync();
			return history;
		}
		[HttpPost("[action]")]
		public async Task ClearCache()
		{
			await userCache.ClearCacheAsync();
		}
	}
}
