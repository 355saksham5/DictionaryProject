using DictionaryApi.Helpers;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace DictionaryApi.Models
{
	public class ErrorModel
	{
		[JsonPropertyName("ErrorCode")]
		public int ErrorCode { get; set; }
        [JsonPropertyName("ErrorMessage")]
        public string? ErrorMessage { get; set; }
        [JsonPropertyName("ErrorDetails")]
        public string? ErrorDetails { get; set; }
        public ErrorModel()
        {
            
        }

        public ErrorModel(int errCode, string errMssg, string errDetails = null!)
		{
			ErrorCode = errCode;
			ErrorMessage = OverWriteErrorMssgs(errCode) ?? errMssg;
			ErrorDetails = errDetails;
		}
		private static string? OverWriteErrorMssgs(int errCode)
		{
			switch (errCode)
			{
				case (int)HttpStatusCode.NotFound:
					return ConstantResources.wordNotFoundErr;
				default:
					return null;
            }
		}
	}
}
