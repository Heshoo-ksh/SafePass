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
builder.Services.AddSingleton<IMediatorService, MediatorService>();

// Add MudBlazor services
builder.Services.AddMudServices();
builder.Services.AddScoped<AuthState>();
builder.Services.AddTransient<LoginService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<CreditCardService>();
builder.Services.AddTransient<IdentityService>();
builder.Services.AddTransient<NoteService>();

// Add a DB context factory to the services of our application, which means we can use it as part of dependency injection elsewhere in the app
builder.Services.AddDbContextFactory<SafePassContext>(opt =>
    opt.UseSqlite($"Data Source={nameof(SafePassContext.SafePassDb)}.db"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
     app.UseExceptionHandler("/Error");
     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
     app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();