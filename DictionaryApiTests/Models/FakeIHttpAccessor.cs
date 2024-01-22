using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApiTests.Models
{
    public class FakeIHttpAccessor : IHttpContextAccessor
    {
        public HttpContext? HttpContext
        {
            set;
            get;
        }

        public FakeIHttpAccessor()
        {
            HttpContext = new FakeHttpContext();
        }
        

    }
}
