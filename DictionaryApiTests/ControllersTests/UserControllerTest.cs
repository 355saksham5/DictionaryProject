using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Controllers;
using DictionaryApi.Models;
using DictionaryApiTests.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApiTests.ControllersTests
{
    
    [TestClass]
    public class UserControllerTest
    {
        Mock<UserManager<IdentityUser>> userManager = new Mock<UserManager<IdentityUser>>(
        new Mock<IUserStore<IdentityUser>>().Object,null,null,null,null,null,null,null,null);
        private static Mock<IJwtTokenService> jwt = new Mock<IJwtTokenService>();
        private static UserController userController;
        public UserControllerTest()
        {
            userManager.Object.UserValidators.Add(new UserValidator<IdentityUser>());
            userManager.Object.PasswordValidators.Add(new PasswordValidator<IdentityUser>());
            userController = new UserController(userManager.Object, jwt.Object);
        }
        [TestMethod]
        [DataRow("new1@gmail.com","Qwert1.")]
        public async Task Register_OnValidRegisterModel_ReturnCreated(string Email, string Password)
        {
            userManager.Setup(x=>x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync((IdentityResult)new FakeIdentityResult(true));
            var actual = ( userController.Register(new RegisterModel { Email = "", Password = "" }).Result) as CreatedAtActionResult;
            Assert.AreEqual(201, actual?.StatusCode);
            Assert.AreEqual(true, (actual.Value as UserIdentityResult).Succeeded);
        }
        [TestMethod]
        [DataRow("new1@gmail.com", "Qwert1.")]
        public async Task Register_OnInValidRegisterModel_ReturnBadRequest(string Email, string Password)
        {
            userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync((IdentityResult)new FakeIdentityResult(false));
            var actual = (userController.Register(new RegisterModel { Email = "", Password = "" }).Result) as CreatedAtActionResult;
            Assert.AreEqual(400, actual?.StatusCode);
            Assert.AreEqual(false, (actual.Value as UserIdentityResult).Succeeded);
        }
        [TestMethod]
        public async Task   LogIn_OnValidLoginCred_ReturnOkJwt()
        {
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
               .ReturnsAsync(new IdentityUser());
            userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            var actual = (userController.Login(new LoginModel { Email = "", Password = "" }).Result);
            Assert.IsNotNull(((actual as OkObjectResult)?.Value as string));
        }
        [TestMethod]
        public async Task LogIn_OnWrongPassword_ReturnUnauthorized()
        {
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new IdentityUser());
            userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<IdentityUser>(),It.IsAny<string>()))
                .ReturnsAsync(false);
            var actual = (userController.Login(new LoginModel { Email = "", Password = "" }).Result);
            Assert.IsNotNull(((actual as UnauthorizedObjectResult)?.Value as LogInResult));
        }
        [TestMethod]
        public async Task LogIn_OnUserNotExist_ReturnHttpNotFound()
        {
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((IdentityUser)null);
            var actual = (userController.Login(new LoginModel { Email = "", Password = "" }).Result);
            Assert.IsNotNull(((actual as NotFoundObjectResult)?.Value as LogInResult));

        }
    }
}
