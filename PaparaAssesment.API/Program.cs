using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PaparaAssesment.Repository.Models;
using PaparaAssesment.Repository.Models.Apartments;
using PaparaAssesment.Repository.Models.Buildings;
using PaparaAssesment.Repository.Models.Payments;
using PaparaAssesment.Repository.Models.User;
using PaparaAssesment.Service.Services.Apartments;
using PaparaAssesment.Service.Services.Buildings;
using PaparaAssesment.Service.Services.Payments;
using PaparaAssesment.Service.Services.Token;
using PaparaAssesment.Service.Services.User;
using PaparaAssesment.Service.UnitOfWorks;
using System.Text;
using Azure.Core;
using System.Collections.Generic;
using PaparaAssesment.API;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<SignInService>();
builder.Services.AddScoped<IApartmentRepository, ApartmentRepositoryWithSql>();
builder.Services.AddScoped<IApartmentService, ApartmentServiceWithSql>();
builder.Services.AddScoped<IBuildingRepository, BuildingRepositoryWithSql>();
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepositoryWithSql>();
builder.Services.AddScoped<IPaymentService,PaymentServiceWithSql>();
builder.Services.AddScoped<PaymentHelper>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), b => b.MigrationsAssembly("PaparaAssesment.Repository")));

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    var signatureKey = builder.Configuration.GetSection("TokenOptions")["SignatureKey"]!;
    var issuer = builder.Configuration.GetSection("TokenOptions")["Issuer"]!;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = true,
        ValidIssuer = issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey))
    };
});

builder.Services.AddAutoMapper(typeof(Program)); // IMapper

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    var appRole = new AppRole
    {
        Name = "Admin"
    };
    IdentityResult? roleCreateResult = null;
    if (!await roleManager.RoleExistsAsync(appRole.Name))
    {
        roleCreateResult = await roleManager.CreateAsync(appRole);
    }
    if (roleCreateResult is not null && !roleCreateResult.Succeeded)
    {
        Console.WriteLine($"Admin role is not created");
    }
    var ResidanceRole = new AppRole
    {
        Name = "Residance"
    };
    IdentityResult? roleResidanceResult = null;
    if (!await roleManager.RoleExistsAsync(ResidanceRole.Name))
    {
        roleResidanceResult = await roleManager.CreateAsync(ResidanceRole);
    }
    if (roleResidanceResult is not null && !roleResidanceResult.Succeeded)
    {
        Console.WriteLine($"Residance role is not created");
    }

    var usersInAdminRole = await userManager.GetUsersInRoleAsync("Admin");
    if (usersInAdminRole is null || usersInAdminRole.Count == 0)
    {
        var AdminUser = new AppUser
        {
            UserName = "admin",
            TCNo = "10000000000",
            Email = "admin@gmail.com"

        };
        var result = await userManager.CreateAsync(AdminUser, "AdminPassword.12");

        if (!result.Succeeded)
        {
            Console.WriteLine($"Admin user is not created");
        }
        var roleAssignResult = await userManager.AddToRoleAsync(AdminUser, "Admin");
        if (roleAssignResult.Succeeded)
        {
            Console.WriteLine($"Admi E-mail: {AdminUser.Email}");
            Console.WriteLine($"Admin password: AdminPassword.12");
        }
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
