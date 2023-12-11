namespace DictionaryApp.Helpers
{
	public static class ConstantResources
	{
		public static readonly string loginUrl = "https://localhost:7060/Account/LogIn";
		public static readonly string[] allowedGetUrls = new string[]
		{
			"https://localhost:7060/Account/LogIn",
			"https://localhost:7060/account/register",
			"https://localhost:7060/lib/jquery-validate/jquery.validate.js"
	};
		public static readonly string loginClaimKey = "isLoggedIn";
		public static readonly string loginClaimValue = "true";
		public static readonly double expiresInDays = 1;
		public static readonly string cookieName = "Authorization";
		public static readonly string getBaseAddressDictApi = "BaseAddresses:DictionaryApi";
		public static readonly string errPagePath = "/Error";
        public static readonly string exceptionPagePath = "/Error/{0}";
		public const string passwordMismatchErr = "Password and confirmation password do not match.";
		public const string cookieIdentifier = "Bearer";
        public const string userNotFoundErr = "User Not Found";
        public const string wrongCredErr = "Invalid Login Attempt";
		public const string notFoundErr = "Sorry, the resource you requested could not be found";
		public static readonly string errorPageUrl = "https://localhost:7060/Error";
        public static readonly string wordNotExistPageUrl = "https://localhost:7060/Error/WordNotFound";
    }
}
