using ProjetS04_API.Models;
using ProjetS04_API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AspNetCore.Authentication.Basic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ProjetS04DatabaseSettings>(
builder.Configuration.GetSection("ProjetS04Database"));
builder.Services.AddSingleton<ClientsService>();
builder.Services.AddSingleton<ReservationsService>();
builder.Services.AddSingleton<VolsService>();
// Add services to the container.

builder.Services.AddAuthorization();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Add services to the container.
builder.Services.AddControllers()
 .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors();



app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();