using FlowerShopLibrary;
using HarapanFF.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

string credentialPath = @"C:\Users\User\source\repos\HarapanFF\flowerorderdatabase-firebase-adminsdk-n6zxd-90aae6e66c.json";
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<FlowerProductList>();
builder.Services.AddSingleton<OrderList>();
builder.Services.AddSingleton<OrderService>();
builder.Services.AddSingleton<FlowerShopBusiness>();



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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
