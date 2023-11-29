using DictionaryApi.BusinessLayer.Services;
using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.ExternalApiHandlers.IExternalApiHandlers;
using DictionaryApi.Helpers;
using DictionaryApi.Models;
using DictionaryApi.Models.DTOs;
using Microsoft.AspNetCore.Identity;
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DictionaryDBConnection")));
builder.Services.AddRefitClient<IMeaningApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["BaseAddresses:MeaningApi"]));
builder.Services.AddRefitClient<ISuggestionApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["BaseAddresses:SuggestionApi"]));
builder.Services.AddMvc();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddScoped<IBasicWordDetailsRepo, BasicWordDetailRepo>();
builder.Services.AddScoped<IWordDetailsService, WordDetailsService>();
builder.Services.AddScoped<IPhoneticAudiosRepo, PhoneticAudiosRepo>();
builder.Services.AddScoped<IDefinitionsRepo, DefinitionRepo>();
builder.Services.AddScoped<IAntonymsRepo, AntonymsRepo>();
builder.Services.AddScoped<ISynonymsRepo, SynonymsRepo>();
builder.Services.AddScoped<BasicWordDetails>();
builder.Services.AddScoped<UserIdentityResult>();
builder.Services.AddScoped<ICache, Cache>();
builder.Services.AddScoped<IMeaningApiMapper, MeaningApiMapper>();
builder.Services.AddScoped<ISuggestionService,SuggestionService>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
