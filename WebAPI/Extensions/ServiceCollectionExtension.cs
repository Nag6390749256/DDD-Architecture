using Domain.Entities;
using Domain.Interface;
using Domain.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI.AppCode.Identity;

namespace WebAPI.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterService(this IServiceCollection services, IConfiguration configuration)
        {
            string dbConnectionString = configuration.GetConnectionString("SqlConnection");
            IConnectionString ch = new ConnectionProvidor { connectionString = dbConnectionString };
            services.AddSingleton<IConnectionString>(ch);
            /* Domain Layer Services */
            services.AddSingleton<IDapperRepository, DapperRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            /* Application Layer Services */
            services.AddScoped<Applications.Interface.IUserService, Applications.Services.UserService>();
            services.AddScoped<Applications.Interface.ITokenService, Applications.Services.TokenService>();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Version = "v1.1",
                    Title = "API Documentation(v1.1)"
                });
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ApiDoc.xml");
                option.IncludeXmlComments(filePath);
                option.UseAllOfToExtendReferenceSchemas();
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Standard authorization header using the bearer scheme(\"Bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            #region Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.AllowedForNewUsers = false;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.User.RequireUniqueEmail = false;
            }).AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>()
            .AddUserManager<ApplicationUserManager>()
            .AddDefaultTokenProviders();
            services.AddAuthentication(option =>
            {
                option = new Microsoft.AspNetCore.Authentication.AuthenticationOptions
                {
                    DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme,
                    DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme,
                    DefaultScheme = JwtBearerDefaults.AuthenticationScheme
                };
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secretkey"]))
                };
            });
            #endregion
            services.Configure<JWTConfig>(configuration.GetSection("JWT"));
        }
    }
}
