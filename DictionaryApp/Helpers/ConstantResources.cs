namespace DictionaryApp.Helpers
{
	public static class ConstantResources
	{
		public static readonly string loginUrl = "http://localhost:7060/Account/LogIn";
		public static List<string> allowedUrls = new List<string>
		{
          "http://localhost:7060/Account/LogIn",
          "http://localhost:7060/account/login",
          "http://localhost:7060/account/register"
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
		public static readonly string errorPageUrl = "http://localhost:7060/Error";
        public static readonly string wordNotExistPageUrl = "http://localhost:7060/Error/WordNotFound";
		public const string regexPassword = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$";
        public const string errMssgPasswordFormat = "Invalid Input Format For This Field";
        public const string errMssgRequired = " is Required";
		public static string homeUrl = "http://localhost:7060/Home/Index";
    }
}
