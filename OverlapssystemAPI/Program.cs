using OverlapssystemAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using OverlapssystemDomain.Interfaces;
using OverlapssystemInfrastructure.Repositories;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IResidentRepository, ResidentRepository>();
builder.Services.AddScoped<IResidentServices, ResidentServices>();
builder.Services.AddScoped<IMedicinService, MedicinService>();
builder.Services.AddScoped<IMedicinRepository, MedicinRepository>();
builder.Services.AddScoped<IPNMedicinService, PNMedicinService>();
builder.Services.AddScoped<IPNMedicinRepository, PNMedicinRepository>();
builder.Services.AddScoped<IShoppingService, ShoppingService>();
builder.Services.AddScoped<IShoppingRepository, ShoppingRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentTaskRepository, DepartmentTaskRepository>();
builder.Services.AddScoped<IDepartmentTaskService, DepartmentTaskService>();
builder.Services.AddScoped<ISpecialEventRepository,  SpecialEventRepository>();
builder.Services.AddScoped<ISpecialEventService, SpecialEventService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddDbContext<OverlapssystemInfrastructure.Data.OverlapDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjektDB"))); //Tilføjer DbContext og konfigurerer den til at bruge SQL Server med en forbindelse streng fra appsettings.json



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Global error handling (tidlig)
app.UseMiddleware<ExceptionHandlingMiddleware>();

//Secuity/ transport
app.UseHttpsRedirection();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Endpoints
app.MapControllers();

app.Run();
