namespace DictionaryApp.Extension
{
	public static class StringExtensions
	{
		public static bool CustomEquals(this string a, string b)
		{
			var result = String.Compare(a, b, StringComparison.InvariantCultureIgnoreCase);
			return result == 0;
		}
	}
}
