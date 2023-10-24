using System.Text;
using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using Core.DomainServices.Repositories;
using Core.DomainServices.Repositories.Interfaces;
using Core.DomainServices.Services;
using Core.DomainServices.Services.Interfaces;
using dotenv.net;
using DotNetEnv;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.GraphQL;
using static HotChocolate.SchemaBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load environment variables
DotEnv.Load();

// Get the password from the environment variables
var password = Env.GetString("DB_PASSWORD");

var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext")!
    .Replace("{password}", password);

var identityConnectionString = builder.Configuration.GetConnectionString("IdentityDbContext")!
    .Replace("{password}", password);

// Configure and add your ApplicationDbContext for application data
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)); // Use the modified connectionString

// Configure and add your IdentityDbContext for identity data
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(identityConnectionString)); // Use the modified identityConnectionString

builder.Services.AddIdentity<Person, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters.ValidateAudience = false;
    options.TokenValidationParameters.ValidateIssuer = false;
    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["BearerTokens:Key"]!));
});


builder.Services.AddScoped<IBoardGameNightService, BoardGameNightService>();
builder.Services.AddScoped<IBoardGameNightRepository, BoardGameNightRepository>();
builder.Services.AddScoped<IBoardGameService, BoardGameService>();
builder.Services.AddScoped<IBoardGameRepository, BoardGameRepository>();


builder.Services.AddGraphQLServer()
    .RegisterService<BoardGameNightRepository>()
    .AddQueryType<Query>();



var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.MapGraphQL();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();