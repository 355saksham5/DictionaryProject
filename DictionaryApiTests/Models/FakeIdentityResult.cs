using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApiTests.Models
{
    public class FakeIdentityResult : IdentityResult
    {
        public FakeIdentityResult(bool Succeded)
        {
            this.Succeeded = Succeded;
        }
    }
}
