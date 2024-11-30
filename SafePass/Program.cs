using SafePass.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services; // MudBlazor services

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddControllers();

// Configure SQLite database context.
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlite("Data Source=users.db"));

// Register the UserService.
builder.Services.AddScoped<UserService>();
builder.Services.AddHttpClient();

// Register MudBlazor services


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
