namespace DictionaryApi.Helpers
{
	public static class ConstantResources
	{
		public const int limitOfCache = 5;
		public static readonly string errorOnInvalidWordId = "InValid Word Id";
		public static readonly string getBaseAddressMeaningApi = "BaseAddresses:MeaningApi";
        public static readonly string getHangfire = "HangFireDBConnection";
        public static string getConnectionString = "DictionaryDBConnection";
        public static string errOnIndexNull="Index is Null";
        public static string errorOnInvalidIndex="Index out of Bound";
        public static readonly string getBaseAddressSuggestionApi = "BaseAddresses:SuggestionApi";
		public const string jwtSectionName = "JwtConfiguration";
        public static readonly string exceptionResponseType = "application/json";
        public static readonly string wordNotFoundErr = "Given Word not found";
		public static readonly string claimInJwt = "UserId";
		public static readonly string userCacheNavProp = "Cache";
        public const string apiVersion = "1.0";
        public static readonly double expiresInDays = 1;
        public const string passwordMismatchErr = "Password and confirmation password do not match.";
		public const string errOnWordIdNull = "Word Id is Null";
		public const string errOnInvalidWordId = "Word Id is Invalid";
        public const int timeWindow = 6;
        public static bool isAppStarting = true;
        public const int maxAgeCache = 86400;


    }
}
