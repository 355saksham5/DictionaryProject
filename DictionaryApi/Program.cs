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
using Hangfire;
using Hangfire.Common;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refit;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApiVersioning(x =>
{
	x.DefaultApiVersion = new ApiVersion(1, 0);
	x.AssumeDefaultVersionWhenUnspecified = true;
	x.ReportApiVersions = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString(ConstantResources.getConnectionString)));
builder.Services.AddRefitClient<IMeaningApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration[ConstantResources.getBaseAddressMeaningApi]));
builder.Services.AddRefitClient<ISuggestionApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration[ConstantResources.getBaseAddressSuggestionApi]));
builder.Services.AddHangfire(option => option.UseMemoryStorage());
builder.Services.AddHangfireServer();

builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(option => { option.User.RequireUniqueEmail = true; }).AddEntityFrameworkStores<AppDbContext>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
builder.Services.AddJwtTokenServices(builder.Configuration);


builder.Services.AddScoped<IBasicWordDetailsRepo, BasicWordDetailRepo>();
builder.Services.AddScoped<IPhoneticAudiosRepo, PhoneticAudiosRepo>();
builder.Services.AddScoped<IDefinitionsRepo, DefinitionRepo>();
builder.Services.AddScoped<IAntonymsRepo, AntonymsRepo>();
builder.Services.AddScoped<ISynonymsRepo, SynonymsRepo>();
builder.Services.AddScoped<IUserCacheRepo, UserCacheRepo>();

builder.Services.AddScoped<BasicWordDetails>();
builder.Services.AddScoped<UserIdentityResult>();
builder.Services.AddScoped<LogInResult>();
builder.Services.AddScoped<CachedWord>();

builder.Services.AddScoped<ICache, Cache>();
builder.Services.AddScoped<IWordDetailsService, WordDetailsService>();
builder.Services.AddScoped<IMeaningApiMapper, MeaningApiMapper>();
builder.Services.AddScoped<ISuggestionService, SuggestionService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUserCacheService, UserCacheService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.Use(async (context, next) =>
{
	if(ConstantResources.flag==0)
	{
        RecurringJob.AddOrUpdate("clearCache", (ICache cache) => cache.DeleteFromCache(), Cron.Daily);
		ConstantResources.flag = 1;
    }
    await next();
});
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
