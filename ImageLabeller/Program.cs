using ImageLabeller.Dals;
using ImageLabeller.Models;
using ImageLabeller.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

await ConfigLoader.GetInstance().Init();
ImageLabellerGlobals globals = await ConfigLoader.GetInstance().GetGlobals();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");


PostgresDal.Init(new PostgresConfig(
    globals.PostgresHost,
    globals.PostgresUserName,
    globals.PostgresPassword,
    globals.PostgresDatabase)
);


app.Run();