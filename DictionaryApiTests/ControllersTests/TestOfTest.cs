using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApiTests.ControllersTests
{
    [TestClass]
    public class TestOfTest
    {
        public UserControllerTest  x = new UserControllerTest();
        [TestMethod]
        public async Task Test_Register_OnValidRegisterModel_ReturnCreated()
        {
            await x.Register_OnValidRegisterModel_ReturnCreated("", "");
            Assert.IsTrue(true);
        }
    }
}
