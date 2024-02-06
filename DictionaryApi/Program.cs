using DictionaryApi.BusinessLayer.Services;
using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Extensions;
using DictionaryApi.ExternalApiHandlers.IExternalApiHandlers;
using DictionaryApi.Helpers;
using DictionaryApi.Middlewares;
using DictionaryApi.Models;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.UserCache;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.Common;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refit;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddFluentValidation(config => config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddApiVersioning(x =>
{
	x.DefaultApiVersion = new ApiVersion(1, 0);
	x.AssumeDefaultVersionWhenUnspecified = true;
	x.ReportApiVersions = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuthorize();
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString(ConstantResources.getConnectionString)));
builder.Services.AddRefitClient<IMeaningApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration[ConstantResources.getBaseAddressMeaningApi]));
builder.Services.AddRefitClient<ISuggestionApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration[ConstantResources.getBaseAddressSuggestionApi]));
builder.Services.AddHangfire(option => option.UseMemoryStorage());
builder.Services.AddHangfireServer();
builder.Services.AddCors(o => o.AddPolicy("TestPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(option => {  }).AddEntityFrameworkStores<AppDbContext>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
builder.Services.AddJwtTokenServices(builder.Configuration);


builder.Services.AddScoped<IBasicWordDetailsRepository, BasicWordDetailRepository>();
builder.Services.AddScoped<IPhoneticAudioRepository, PhoneticAudiosRepository>();
builder.Services.AddScoped<IDefinitionsRepository, DefinitionRepository>();
builder.Services.AddScoped<IAntonymsRepository, AntonymsRepository>();
builder.Services.AddScoped<ISynonymsRepository, SynonymsRepository>();
builder.Services.AddScoped<IUserCacheRepository, UserCacheRepository>();
builder.Services.AddScoped<DataSeeder>();

builder.Services.AddScoped<ICache, Cache>();
builder.Services.AddScoped<IWordDetailsService, WordDetailsService>();
builder.Services.AddScoped<IMeaningApiMapper, MeaningApiMapper>();
builder.Services.AddScoped<ISuggestionService, SuggestionService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUserCacheService, UserCacheService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var dataSeeder = services.GetRequiredService<DataSeeder>();
    if (context.Database.GetPendingMigrations().Any() && app.Environment.IsEnvironment("Testing"))
    {
        context.Database.Migrate();
        await dataSeeder.Seed();
    }
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("TestPolicy");
if (ConstantResources.isAppStarting)
{
	app.Use(async (context, next) =>
	{
		RecurringJob.AddOrUpdate("clearCache", (ICache cache) => cache.DeleteFromCache(), Cron.Daily);
		ConstantResources.isAppStarting = false;
		await next();
	});
}
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
