using EMS_SYSTEM.APPLICATION;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.GenericRepository;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.APPLICATION.Repositories.Services;
using EMS_SYSTEM.APPLICATION.Repositories.Services.UnitOfWork;
using EMS_SYSTEM.DOMAIN.Enums;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger Configurations
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "EMS",

    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {

        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter You JWT Key"

    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer",
                },Name="Bearer",
                In=ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Configure Connection String and Inject It
builder.Services.AddDbContext<UnvcenteralDataBaseContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure The Identity Tables
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<UnvcenteralDataBaseContext>()
    .AddDefaultTokenProviders()
    .AddSignInManager<SignInManager<ApplicationUser>>();

// Add Authentication Service
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ValidIssuer = builder.Configuration["JWT:issuer"],
        ValidAudience = builder.Configuration["JWT:audience"],
        ClockSkew = TimeSpan.Zero
    };

});

// Inject Services 
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ObserversAndInvigilatorsService>();

// add cores
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();


    foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
    {
        if (!await roleManager.RoleExistsAsync(role.ToString()))
        {
            await roleManager.CreateAsync(new IdentityRole(role.ToString()));
        }
    }

    ApplicationUser user = new()
    {
        NID = "30307052100754",
        UserName = "FacultyAdmin1",
        Email = "FacultyAdmin1@gmail.com",
        LockoutEnabled = false,
        PhoneNumber = "0123456789"
    };
    ApplicationUser user2 = new()
    {
        NID = "40370921027543",
        UserName = "Student1",
        Email = "Student1@gmail.com",
        LockoutEnabled = false,
        PhoneNumber = "01111111111"
    };
    ApplicationUser user3 = new()
    {
        NID = "36707052155754",
        UserName = "Globeladmin1",
        Email = "Globeladmin1@gmail.com",
        LockoutEnabled = false,
        PhoneNumber = "01478523698"
    };

    if (await userManager.FindByIdAsync(user.NID) == null)
    {


        var result = await userManager.CreateAsync(user, "FacultyAdmin*123");
        var result2 = await userManager.CreateAsync(user2, "Student1*123");
        var result3 = await userManager.CreateAsync(user3, "Globeladmin1*123");


        // Add user to roles
        await userManager.AddToRoleAsync(user, "FacultyAdmin");
        await userManager.AddToRoleAsync(user2, "Student");
        await userManager.AddToRoleAsync(user3, "GlobelAdmin");
    }



}

app.Run();
