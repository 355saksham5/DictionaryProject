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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddRefitClient<IMeaningApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration[ConstantResources.getBaseAddressMeaningApi]));
builder.Services.AddRefitClient<ISuggestionApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration[ConstantResources.getBaseAddressSuggestionApi]));
builder.Services.AddMvc();
builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddScoped<IBasicWordDetailsRepo, BasicWordDetailRepo>();
builder.Services.AddScoped<IWordDetailsService, WordDetailsService>();
builder.Services.AddScoped<IPhoneticAudiosRepo, PhoneticAudiosRepo>();
builder.Services.AddScoped<IDefinitionsRepo, DefinitionRepo>();
builder.Services.AddScoped<IAntonymsRepo, AntonymsRepo>();
builder.Services.AddScoped<ISynonymsRepo, SynonymsRepo>();
builder.Services.AddScoped<BasicWordDetails>();
builder.Services.AddScoped<UserIdentityResult>();
builder.Services.AddScoped<LogInResult>();
builder.Services.AddScoped<ICache, Cache>();
builder.Services.AddScoped<CachedWord>();
builder.Services.AddScoped<IMeaningApiMapper, MeaningApiMapper>();
builder.Services.AddScoped<ISuggestionService,SuggestionService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUserCacheService, UserCacheService>();
builder.Services.AddScoped<IUserCacheRepo, UserCacheRepo>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
builder.Services.AddJwtTokenServices(builder.Configuration);
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();



app.Run();
