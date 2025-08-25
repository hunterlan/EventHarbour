using EventHarbour.UserService.Presentation.Helpers;
using EventHarbour.UserService.Presentation.Options;
using EventHarbour.UserService.Presentation.Services.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("UserServiceContext");

if (connectionString is null)
{
    throw new ApplicationException("UserServiceContext connection string is not specified.");
}

var jwtSection = builder.Configuration.GetSection(JwtOptions.Jwt);

if (jwtSection is null)
{
    throw new ApplicationException("Jwt section is not specified.");
}

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContextPool<UserContext>(opt => 
    opt.UseNpgsql(connectionString));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddOptions<JwtOptions>()
    .Bind(jwtSection)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();