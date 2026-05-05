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
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy("AllowBlazorApp",
      builder => builder.WithOrigins("https://localhost:7239")
                       .AllowAnyHeader()
                       .AllowAnyMethod());
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
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
builder.Services.AddIdentityCore<UserModel>(options =>
{
    // Password-krav 
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    //options.Password.RequiredLength = 8;
})
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
builder.Services.AddScoped<IAuditTrailDetailService, AuditTrailDetailService>();
builder.Services.AddScoped<IAuditTrailDetailRepository, AuditTrailDetailRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

//Seeder nogle roller så alle kan komme ind. Skal slettes når kunden får programmet
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserModel>>();

    // Seed roller
    string[] roles = { "Simpel", "Medarbejder", "Administrator" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    // Seed brugere
    var users = new[]
    {
        new { UserName = "Slot", Password = "Slot123@", FirstName = "Slot", LastName = "Slottet", DepartmentId = 1, Role = "Simpel" },
        new { UserName = "Skov", Password = "Skov123@", FirstName = "Skov", LastName = "Skoven", DepartmentId = 2, Role = "Simpel" },
        new { UserName = "Medarbejder1", Password = "Test123@", FirstName = "Afdeling1", LastName = "Afdeling1", DepartmentId = 1, Role = "Medarbejder" },
        new { UserName = "Medarbejder2", Password = "Test123@", FirstName = "Afdeling2", LastName = "Afdeling2", DepartmentId = 2, Role = "Medarbejder" },
        new { UserName = "Admin1", Password = "Test123@", FirstName = "Admin1", LastName = "Admin1", DepartmentId = 1, Role = "Administrator" },
        new { UserName = "Admin2", Password = "Test123@", FirstName = "Admin2", LastName = "Admin2", DepartmentId = 2, Role = "Administrator" },
    };

    foreach (var u in users)
    {
        if (await userManager.FindByNameAsync(u.UserName) == null)
        {
            var user = new UserModel
            {
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DepartmentId = u.DepartmentId
            };
            await userManager.CreateAsync(user, u.Password);
            await userManager.AddToRoleAsync(user, u.Role);
        }
    }
}

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
