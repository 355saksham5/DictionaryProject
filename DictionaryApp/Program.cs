using Azure.Core;
using DictionaryApi.Extensions;
using DictionaryApp.Middlewares;
using DictionaryApp.Helpers;
using DictionaryApp.Services;
using Refit;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddRefitClient<IDictionaryApi>().ConfigureHttpClient(c =>
{
	c.BaseAddress = new Uri(builder.Configuration[ConstantResources.getBaseAddressDictApi]);
}).AddHttpMessageHandler<AuthHeaderHandler>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
	AddCookie(options =>
	{
		options.LoginPath = "/account/login";
		options.Cookie.HttpOnly = true;
		options.Cookie.Name = "DictionaryCookie";
	});
builder.Services.AddAuthorization();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{ 
	app.UseMiddleware<ExceptionHandlerMiddleware>();
	app.UseStatusCodePagesWithReExecute(ConstantResources.errPagePath+"/{0}");
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}");

app.Run();
