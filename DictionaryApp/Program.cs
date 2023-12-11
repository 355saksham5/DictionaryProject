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

var builder = WebApplication.CreateBuilder(args);
var options = new JsonSerializerOptions()
{
	PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	WriteIndented = true,
};

var settings = new RefitSettings()
{
	ContentSerializer = new SystemTextJsonContentSerializer(options)
};
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
builder.Services.AddRefitClient<IDictionaryApi>(settings).ConfigureHttpClient(c =>
{
	c.BaseAddress = new Uri(builder.Configuration[ConstantResources.getBaseAddressDictApi]);
}).AddHttpMessageHandler<AuthHeaderHandler>();
builder.Services.AddMvc();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
	app.UseMiddleware<ExceptionHandlerMiddleware>();
	app.UseStatusCodePagesWithReExecute($"{ConstantResources.errPagePath}"+"/{0}");
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<RedirectionMiddleware>();
app.UseRouting();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Default}");

app.Run();
