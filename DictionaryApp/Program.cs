using DictionaryApi.Extensions;
using Refit;
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
builder.Services.AddControllersWithViews();
builder.Services.AddRefitClient<IDictionaryApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["BaseAddresses:DictionaryApi"]));
builder.Services.AddJwtTokenServices(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddMvc();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Default}/{id?}");

app.Run();
