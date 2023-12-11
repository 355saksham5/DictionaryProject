﻿using DictionaryApi.Models.DTOs;

namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
	public interface IAntonymsRepo
	{
		public Task AddAntonymsAsync(Antonyms antonyms);

		public Task<IEnumerable<string>> GetAntonymsAsync(Guid BasicWordDetailsId);
	}
}
