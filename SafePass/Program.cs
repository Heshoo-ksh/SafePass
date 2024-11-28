using SafePass.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// Add this line to register controllers
builder.Services.AddControllers();

// Configure SQLite database context.
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlite("Data Source=users.db")); // Specify your SQLite database file name

// Register the UserService.
builder.Services.AddScoped<UserService>();

// Add HttpClient service (we'll need this for API calls)
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add this line to map controllers
app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
