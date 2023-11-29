using Microsoft.AspNetCore.Identity;

namespace DictionaryApi.Models
{
    public class UserIdentityResult
    {
        public bool Succeeded { get; set; }
        public IEnumerable<IdentityError>? Errors { get; set; }
    }
}
