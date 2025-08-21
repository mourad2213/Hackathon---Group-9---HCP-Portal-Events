using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.DataAccess.Reposatiores;
using HCP_Portal_Events.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<ApplicationDbContext>((provider, options) =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        options =>
        {
            options.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        });
});

builder.Services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IUserRepositiories, UserRepositories>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.UseCors("AllowAll");

app.MapControllers();

// Redirect root ("/") to Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

app.Run();
