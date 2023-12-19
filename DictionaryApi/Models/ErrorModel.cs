using Newtonsoft.Json;
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
			ErrorMessage = errMssg;
			ErrorDetails = errDetails;
		}
	}
}
