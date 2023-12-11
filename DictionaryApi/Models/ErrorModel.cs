namespace DictionaryApi.Models
{
	public class ErrorModel
	{
		public int ErrorCode { get; set; }
		public string? ErrorMessage { get; set; }
		public string? ErrorDetails { get; set; }

		public ErrorModel(int errCode, string errMssg, string errDetails = null!)
		{
			ErrorCode = errCode;
			ErrorMessage = errMssg;
			ErrorDetails = errDetails;
		}
	}
}
