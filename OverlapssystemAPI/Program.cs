using OverlapssystemAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using OverlapssystemDomain.Interfaces;
using OverlapssystemInfrastructure.Repositories;
using OverlapssytemApplication.Interfaces;
using OverlapssytemApplication.Services;
using Microsoft.AspNetCore.Identity;
using OverlapssystemDomain.Entities;
using OverlapssystemInfrastructure.Data;
using OverlapssystemInfrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy("AllowBlazorApp",
      builder => builder.WithOrigins("https://localhost:7239")
                       .AllowAnyHeader()
                       .AllowAnyMethod());
});



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// DbContext
builder.Services.AddDbContext<OverlapDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjektDB")));

// Identity
builder.Services.AddIdentityCore<UserModel>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<OverlapDbContext>()
    .AddDefaultTokenProviders()
    .AddSignInManager();

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
builder.Services.AddScoped<IEmployeePhoneRepository, EmployeePhoneRepository>();
builder.Services.AddScoped<IEmployeePhoneService, EmployeePhoneService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtService>();

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

//CORS
app.UseCors("AllowBlazorApp");

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Endpoints
app.MapControllers();

app.Run();
