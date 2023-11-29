using DictionaryApplication.Dataaccess;
using Refit;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var options = new JsonSerializerOptions()
{
	PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	WriteIndented = true,
};

var settings = new RefitSettings()
{
	ContentSerializer = new SystemTextJsonContentSerializer(options)
};
builder.Services.AddRefitClient<IDictionaryApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["BaseAddresses:DictionaryApi"]));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
