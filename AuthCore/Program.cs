using AuthVerification.Authorization;
using AuthVerification.Core.src.UserFeature.DTOs;
using AuthVerification.Core.src.UserFeature.Interfaces;
using AuthVerification.Core.src.UserFeature.Services;
using AuthVerification.Dbal.DbContexts;
using AuthVerification.Dbal.DbModels;
using AuthVerification.Dbal.Repositories;
using AuthVerification.Core.src.OrganizationsFeature.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Syncfusion.Licensing;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//SyncfusionLicenseProvider.RegisterLicense(
//    "Ngo9BigBOggjHTQxAR8/V1JGaF5cXGpCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWH1cdXRcR2hcVkd0XUpWYEs="
//);
//var licensekey = builder.Configuration["Syncfusion:LicenseKey"];
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
    builder.Configuration["Syncfusion:LicenseKey"]
);

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);
builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddDbContext<EntityDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

var JwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(JwtSettings["key"]);
var issuer = JwtSettings["Issuer"];
var audience = JwtSettings["Audience"];


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = "CrystalSms",
        ValidAudience = "CrystalSmsUsers",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>


{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
});


builder.Services.AddScoped<IAuthorizationHandler, ActiveUserHandler>();



builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserRepositoryInterface, UserRepository>();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped<IPasswordHasher<UserDbModel>, PasswordHasher<UserDbModel>>();
builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IOrganizationRepositoryInterface, OrganizationRepository>();




builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings")
);

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(builder =>
//    builder.AllowAnyOrigin()
//    .AllowAnyHeader()
//    .AllowAnyMethod()
//    );


//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://127.0.0.1:5500", "http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();

        });
});




var app = builder.Build();
app.UseCors();
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CrystalSms API v1");
    c.RoutePrefix = "swagger";
});




using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        await seeder.SeedAsync();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Database migration or seeding failed.");
        throw;
    }
}





if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseExceptionHandler("/Home/Error");
    //app.UseHsts();
}
if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LandingPage}/{action=Index}/{id?}");

app.Run();
