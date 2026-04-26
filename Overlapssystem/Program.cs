using Overlapssystem.Components;
using Overlapssystem.Services;
using OverlapssystemDomain.Interfaces;
using OverlapssystemInfrastructure.Repositories;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;
using Overlapssystem.Facades;
using Overlapssystem.Interfaces;
using Overlapssystem.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// HttpClient
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7150")
});

builder.Services.AddScoped<ResidentApiService>();
builder.Services.AddScoped<MedicinApiService>();
builder.Services.AddScoped<DepartmentApiService>();
builder.Services.AddScoped<PNMedicinApiService>();
builder.Services.AddScoped<ShoppingApiService>();
builder.Services.AddScoped<DepartmentTaskApiService>();
builder.Services.AddScoped<SpecialEventApiService>();
builder.Services.AddScoped<IResidentFacade, ResidentFacade>();
builder.Services.AddScoped<IDepartmentTaskFacade, DepartmentTaskFacade>();
builder.Services.AddScoped<ResidentViewModel>();
builder.Services.AddScoped<DepartmentTaskViewModel>();
builder.Services.AddScoped<MedicinViewModel>();
builder.Services.AddScoped<PNMedicinViewModel>();
builder.Services.AddScoped<ShoppingViewModel>();
builder.Services.AddScoped<SpecialEventViewModel>();
builder.Services.AddScoped<DepartmentViewModel>();


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
