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
builder.Services.AddScoped<AuthenticationStateProvider, AuthState>();
builder.Services.AddScoped<AuthState>();

builder.Services.AddAuthentication("Bearer");

// HttpClient
builder.Services.AddTransient<AuthTokenHandler>();

builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7150");
})
.AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));

builder.Services.AddScoped<AuditTrailDetailApiService>();
builder.Services.AddScoped<ResidentApiService>();
builder.Services.AddScoped<MedicinApiService>();
builder.Services.AddScoped<DepartmentApiService>();
builder.Services.AddScoped<PNMedicinApiService>();
builder.Services.AddScoped<ShoppingApiService>();
builder.Services.AddScoped<DepartmentTaskApiService>();
builder.Services.AddScoped<SpecialEventApiService>();
builder.Services.AddScoped<EmployeePhoneApiService>();
builder.Services.AddScoped<IResidentFacade, ResidentFacade>();
builder.Services.AddScoped<IDepartmentTaskFacade, DepartmentTaskFacade>();
builder.Services.AddScoped<IDepartmentFacade, DepartmentFacade>();
builder.Services.AddScoped<IEmployeePhoneFacade, EmployeePhoneFacade>();
builder.Services.AddScoped<UserApiService>();
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
