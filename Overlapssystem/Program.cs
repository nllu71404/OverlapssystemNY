using Microsoft.AspNetCore.Components.Authorization;
using Overlapssystem.Components;
using Overlapssystem.Services;
using Overlapssystem.TokenService;
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
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<AuthState>());

builder.Services.AddAuthentication("Bearer");

// HttpClient
builder.Services.AddScoped<AuthTokenHandler>();

//builder.Services.AddHttpClient("Api", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7150");
//})
//.AddHttpMessageHandler<AuthTokenHandler>();

//builder.Services.AddScoped(sp =>
//    sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));

builder.Services.AddHttpClient<AuditTrailDetailApiService>(client =>
    client.BaseAddress = new Uri("https://localhost:7150"))
    .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<ResidentApiService>(client =>
    client.BaseAddress = new Uri("https://localhost:7150"))
    .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<MedicinApiService>(client =>
    client.BaseAddress = new Uri("https://localhost:7150"))
    .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<DepartmentApiService>(client =>
    client.BaseAddress = new Uri("https://localhost:7150"))
    .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<PNMedicinApiService>(client =>
    client.BaseAddress = new Uri("https://localhost:7150"))
    .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<ShoppingApiService>(client =>
    client.BaseAddress = new Uri("https://localhost:7150"))
    .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<DepartmentTaskApiService>(client =>
    client.BaseAddress = new Uri("https://localhost:7150"))
    .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<SpecialEventApiService>(client =>
    client.BaseAddress = new Uri("https://localhost:7150"))
    .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<EmployeePhoneApiService>(client =>
    client.BaseAddress = new Uri("https://localhost:7150"))
    .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<UserApiService>(client =>
    client.BaseAddress = new Uri("https://localhost:7150"))
    .AddHttpMessageHandler<AuthTokenHandler>();

// Facades forbliver som de er
builder.Services.AddScoped<IResidentFacade, ResidentFacade>();
builder.Services.AddScoped<IDepartmentTaskFacade, DepartmentTaskFacade>();
builder.Services.AddScoped<IDepartmentFacade, DepartmentFacade>();
builder.Services.AddScoped<IEmployeePhoneFacade, EmployeePhoneFacade>();
builder.Services.AddScoped<IUserFacade, UserFacade>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
