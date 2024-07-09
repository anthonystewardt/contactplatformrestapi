using contactplatformweb;
using contactplatformweb.Endpoints;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Configurar Kestrel para usar la configuración del archivo appsettings.json
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Configure(context.Configuration.GetSection("Kestrel"));
});


var originsHosts = builder.Configuration.GetValue<string>("AllowAccess")!;
// inicio de area de los servicios

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer("name=DefaultConnection");    
});




//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//               builder =>
//               {
//            builder.WithOrigins(originsHosts)
//                   .AllowAnyMethod()
//                   .AllowAnyHeader();
//        });

//    options.AddPolicy("hostFree",
//               builder =>
//               {
//            builder.AllowAnyOrigin()
//                   .AllowAnyMethod()
//                   .AllowAnyHeader();
//        });
//});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });

    options.AddPolicy("hostFree",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();
builder.Services.AddScoped<IRepositorySchedule, RepositorySchedule>();
builder.Services.AddScoped<IRepositoryCalendar, RepositoryCalendar>();
builder.Services.AddScoped<IRepositoryCampaign, RepositoryCampaign>();
builder.Services.AddScoped<IRepositoryCondition, RepositoryCondition>();
builder.Services.AddScoped<IRepositoryPosition, RepositoryPosition>();
builder.Services.AddScoped<IRepositoryState, RepositoryState>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IRepositoryReasonForDepartures, RepositoryReasonForDeparture>();
builder.Services.AddScoped<IRepositoryWeek, RepositoryWeek>();
builder.Services.AddScoped<IRepositorySubCampaign, RepositorySubCampaign>();
builder.Services.AddScoped<IRepositoryModality, RepositoryModality>();
builder.Services.AddScoped<IRepositoryTrainer, RepositoryTrainer>();
builder.Services.AddScoped<IRepositoryCapa, RepositoryCapa>();
builder.Services.AddScoped<IRepositoryTeam, RepositoryTeam>();
builder.Services.AddScoped<IRepositoryCese, RepositoryCese>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.AddAuthorization();
// fin de area de los servicios



var app = builder.Build();

// area de los middlewares
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("hostFree");
app.UseOutputCache();
app.UseAuthentication();
app.UseAuthorization();


var endPointsUsers = app.MapGroup("/users").MapUsers();
var endPointsSchedules = app.MapGroup("/schedules").MapSchedules();
var endPointsCalendars = app.MapGroup("/calendars").MapCalendars();
var endPointsCampaigns = app.MapGroup("/campaigns").MapCampaigns();
var endPointsConditions = app.MapGroup("/conditions").MapConditions();
var endPointsPositions = app.MapGroup("/positions").MapPositions();
var endPointsStates = app.MapGroup("/states").MapStates();
var endPointsReasonForDepartures = app.MapGroup("/reasons-for-departures").MapReasonForDepartures();
var endPointsWeeks = app.MapGroup("/weeks").MapWeeks();
var endPointsSubCampaigns = app.MapGroup("/sub-campaigns").MapSubCampaigns();
var endPointsModalities = app.MapGroup("/modalities").MapModalities();
var endPointsTrainers = app.MapGroup("/trainers").MapTrainers();
var endPointsCapas = app.MapGroup("/capas").MapCapas();
var endPointsTeams = app.MapGroup("/teams").MapTeams();
var endPointsCeses = app.MapGroup("/ceses").MapCeses();
// users



app.Run("http://172.25.120.19:5052");

