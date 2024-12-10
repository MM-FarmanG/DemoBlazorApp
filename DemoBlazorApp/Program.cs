using DemoBlazorApp.Components;
using DemoBlazorApp.Components.Data;
using DemoBlazorApp.Components.Data.DbContext;
using DemoBlazorApp.Components.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string dbConnectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddTransient<IDbConnectionFactory>(e => new DbConnectionFactory(dbConnectionString));
builder.Services.AddSingleton<GlobalDataService>();
builder.Services.AddTransient<IUserAuthentication, UserAuthentication>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
