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

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<AuthState>());
builder.Services.AddAuthentication("Bearer");

builder.Services.AddScoped<AuthTokenHandler>();

builder.Services.AddScoped(sp =>
{
    var authHandler = sp.GetRequiredService<AuthTokenHandler>();
    authHandler.InnerHandler = new HttpClientHandler();

    return new HttpClient(authHandler)
    {
        BaseAddress = new Uri("https://localhost:7150")
    };
});

builder.Services.AddScoped<AuditTrailDetailApiService>();
builder.Services.AddScoped<ResidentApiService>();
builder.Services.AddScoped<MedicinApiService>();
builder.Services.AddScoped<DepartmentApiService>();
builder.Services.AddScoped<PNMedicinApiService>();
builder.Services.AddScoped<ShoppingApiService>();
builder.Services.AddScoped<DepartmentTaskApiService>();
builder.Services.AddScoped<SpecialEventApiService>();
builder.Services.AddScoped<EmployeePhoneApiService>();
builder.Services.AddScoped<UserApiService>();

builder.Services.AddScoped<IResidentFacade, ResidentFacade>();
builder.Services.AddScoped<IDepartmentTaskFacade, DepartmentTaskFacade>();
builder.Services.AddScoped<IDepartmentFacade, DepartmentFacade>();
builder.Services.AddScoped<IEmployeePhoneFacade, EmployeePhoneFacade>();
builder.Services.AddScoped<IUserFacade, UserFacade>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
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