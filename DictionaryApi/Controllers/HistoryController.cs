using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;
using DictionaryApi.Models.UserCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DictionaryApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	[ApiVersion("1.0")]
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
			var history = await userCache.GetCache();
			return history;
		}
		[HttpPost("[action]")]
		public async Task ClearCache()
		{
			await userCache.ClearCache();
		}
	}
}
