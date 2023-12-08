using Azure.Core;
using DictionaryApi.Extensions;
using DictionaryApp.Helpers;
using DictionaryApp.Services;
using Refit;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

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
builder.Services.AddRefitClient<IDictionaryApi>(settings).ConfigureHttpClient(c =>
{
	c.BaseAddress = new Uri(builder.Configuration["BaseAddresses:DictionaryApi"]);
	//c.DefaultRequestHeaders.Add();
}).AddHttpMessageHandler<AuthHeaderHandler>();
builder.Services.AddMvc();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<RedirectionMiddleware>();
app.UseRouting();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Account}/{action=LogIn}");

app.Run();
