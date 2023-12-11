namespace DictionaryApi.Helpers
{
	public static class ConstantResources
	{
		public const int limitOfCache = 5;
		public static readonly string errorOnInvalidWordId = "InValid Word Id";
		public static readonly string getBaseAddressMeaningApi = "BaseAddresses:MeaningApi";
		public static string getConnectionString = "DictionaryDBConnection";
        public static readonly string getBaseAddressSuggestionApi = "BaseAddresses:SuggestionApi";
		public const string configSectionName = "JwtConfiguration";
        public static readonly string exceptionResponseType = "application/json";
        public static readonly string wordNotFoundErr = "Given Word not found";
		public static readonly string claimInJwt = "UserId";
		public static readonly string userCacheNavProp = "Cache";
        public const string apiVersion = "1.0";
    }
}
