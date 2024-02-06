using DictionaryApi.Helpers;
using System.ComponentModel.DataAnnotations;

namespace DictionaryApi.Models
{
    public class RegisterModel
    {
     
        public string? Email { get; set; }


        public string? Password { get; set; }

    }
}
