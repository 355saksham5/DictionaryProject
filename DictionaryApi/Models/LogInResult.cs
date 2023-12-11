namespace DictionaryApi.Models
{
    public class LogInResult
    {
        public string jwt { get; set; } = string.Empty;

        public bool UserConflict { get; set; } = false;

        public bool PasswordFail { get; set; } = false;
    }
}
