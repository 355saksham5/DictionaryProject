using DictionaryApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Refit;

namespace DictionaryApp.Helpers
{
    public static class ExceptionHelper
    {
        public async static Task<T?> ManageExceptions<T>(Func<Task<T?>> func,ITempDataDictionary TempData)
        {
            try 
            {
                return await func(); 
            } 
            catch(ApiException exception)
            {
				var result = await exception.GetContentAsAsync<ErrorModel>();
                TempData["errors"]=result?.ErrorMessage;
				return default;
            }
        }
        public async static Task<T?> ManageExceptionsRegister<T>(Func<Task<T?>> func, ModelStateDictionary ModelState)
        {
            try
            {
                return await func();
            }
            catch (ApiException exception)
            {
                var result = await exception.GetContentAsAsync<UserIdentityResult>();
                foreach (var error in result?.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return default;
            }
        }
        public async static Task<T?> ManageExceptionsLogIn<T>(Func<Task<T?>> func, ModelStateDictionary ModelState)
        {
            try
            {
                return await func();
            }
            catch (ApiException exception)
            {
                var result = await exception.GetContentAsAsync<LogInResult>();
                if (result.PasswordFail)
                {
                    ModelState.AddModelError(string.Empty, ConstantResources.wrongCredErr);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ConstantResources.userNotFoundErr);
                }
                return default;
            }
        }
    }
}
