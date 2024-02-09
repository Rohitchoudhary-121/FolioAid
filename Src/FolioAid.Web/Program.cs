using Application.Commands.Account;
using Application.Commands.Project;
using Application.Utilities;
using Application.Validator;
using Domain.Common;
using Domain.Entity.Identity;
using FluentValidation;
using FluentValidation.AspNetCore;
using FolioAid.ServiceExtensions;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("FolioAidDatabase")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
}).AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();

var jwtIssuer = new JwtIssuerOptions();
builder.Configuration.GetSection("JwtIssuerOptions").Bind(jwtIssuer);
//Create singleton from instance
builder.Services.AddSingleton<JwtIssuerOptions>(jwtIssuer);

var emailSMTPConfiguration = new EmailSMTPConfiguration();
builder.Configuration.GetSection("EmailSMTPConfiguration").Bind(emailSMTPConfiguration);
//Create singleton from instance
builder.Services.AddSingleton<EmailSMTPConfiguration>(emailSMTPConfiguration);

builder.Services.AddApplicationServices();
builder.Services.AddRepositoryServices();


// Add services to the container.

//builder.Services.AddControllersWithViews();
builder.Services.AddControllers()
.AddFluentValidation(x =>
{
    x.ImplicitlyValidateChildProperties = true;
});

//ragister fluint vailidator

builder.Services.AddTransient<IValidator<RegisterUserCommand>, RegisterUserValidator>();
builder.Services.AddTransient<IValidator<GetProjectByIdCommand>, GetProjectByIdValidator>();
builder.Services.AddTransient<IValidator<AddProjectCommand>, AddProjectVailidator>();
builder.Services.AddTransient<IValidator<LoginUserCommand>, LoginUserVailidator>();
var brrOptions = new JwtBearerOptions();
brrOptions.TokenValidationParameters = new TokenValidationParameters
{
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtIssuer.Secret)),

    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidAudiences = jwtIssuer.Audience,
    ValidIssuer = jwtIssuer.Issuer,
    ClockSkew = TimeSpan.Zero,
    ValidateIssuerSigningKey = true,
    RequireSignedTokens = true,
};
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = brrOptions.TokenValidationParameters;

    x.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var token = context.SecurityToken as JwtSecurityToken;
            if (token != null && !token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
