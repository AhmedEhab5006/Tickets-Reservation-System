using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TicketsReservationSystem.API.Helpers;
using TicketsReservationSystem.BLL.Managers;
using TicketsReservationSystem.BLL.Managers.AuthManagers;
using TicketsReservationSystem.BLL.Profiles;
using TicketsReservationSystem.DAL.Database;
using TicketsReservationSystem.DAL.Models;
using TicketsReservationSystem.DAL.Repository;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IClientManager, ClientManager>();
        //builder.Services.AddScoped<IAdminRepository, AdminRepository>();
        builder.Services.AddScoped<IVendorRepository, VendorRepository>();
        builder.Services.AddScoped<IVendorManager, VednorManager>();
        builder.Services.AddScoped<IUserManager, UserManager>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IAuthManager, AuthManager>();
        builder.Services.AddScoped<IGetLoggedData, GetLoggedData>();
        builder.Services.AddScoped<IEmailSender, EmailSender>();
        builder.Services.AddScoped<IMemoryCache , MemoryCache>();
        builder.Services.AddScoped<IApplicationUserRoleRepository, ApplicationUserRoleRepository>();
        builder.Services.AddScoped<RoleManager<ApplicationUserRole>>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAutoMapper(typeof(ClientProfile).Assembly);


        builder.Services.AddIdentity<ApplicationUser, ApplicationUserRole>
           (options =>
           {
               options.Password.RequireDigit = false;
               options.Password.RequiredLength = 5;
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireUppercase = false;
               options.Password.RequireLowercase = false;
           })
                .AddEntityFrameworkStores<ProgramContext>();

        builder.Services.AddDbContext<ProgramContext>(option =>
            
        option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        

        builder.Services.AddScoped<IAuthManager, AuthManager>();

        builder.Services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = "";
            option.DefaultChallengeScheme = "";
        }).AddJwtBearer("", options =>
        {
            var securitykeystring = builder.Configuration.GetSection("SecretKey").Value;
            var securtykeyByte = Encoding.ASCII.GetBytes(securitykeystring);
            SecurityKey securityKey = new SymmetricSecurityKey(securtykeyByte);

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = securityKey,
                //ValidAudience = "url" ,
                //ValidIssuer = "url",
                ValidateIssuer = false,
                ValidateAudience = false ,

                RoleClaimType = ClaimTypes.Role
            };
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new() { Title = "Tickets Reservation System API", Version = "v1" });

            // 🔐 Add JWT Bearer token support
            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer abcdefgh123456\""
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

        var app = builder.Build();

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
    }
}