using SafePass.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

using MudBlazor.Services;
using SafePass.Data;
using SafePass.Services;


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


// Add MudBlazor services

builder.Services.AddTransient<LoginService>();
// Add a DB context factory to the services of our application, which means we can use it as part of dependency injection elsewhere in the app
builder.Services.AddDbContextFactory<SafePassContext>(opt =>
    opt.UseSqlite($"Data Source={nameof(SafePassContext.SafePassDb)}.db"));



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
