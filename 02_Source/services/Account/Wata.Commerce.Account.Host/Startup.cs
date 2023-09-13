using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Wata.Commerce.Common.Middlewares;
using Wata.Commerce.Account.Domain.Repositories;
using Wata.Commerce.Account.Data.Repositories;
using Wata.Commerce.Account.Data.Context;
using Wata.Commerce.Account.Business.MapperProfiles;
using Wata.Commerce.Account.Business.Services;

namespace Wata.Commerce.Account.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            services.AddDbContext<AccountContext>(options => options.UseSqlServer("name=ConnectionStrings:Account", b => b.MigrationsAssembly("Wata.Commerce.Account.MigrationSqlServer")));
            //services.AddScoped<AuthFilter>();
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudiences = Configuration["Jwt:Audiences"].Split(","),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Account Aggregator for Web Clients",
                    Version = "v1",
                    Description = "Account Aggregator for Web Clients"
                });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
              {
                {
                  new OpenApiSecurityScheme
                  {
                    Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                      },
                      Scheme = "oauth2",
                      Name = JwtBearerDefaults.AuthenticationScheme,
                      In = ParameterLocation.Header,

                    },
                    new List<string>()
                  }
                });

            });

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            //services.AddSingleton<ISecurityService, SecurityService>();

            //Repositories
            services.AddScoped<IRoleRepository, RoleRepository>();

            //Business
            services.AddScoped<IRoleService, RoleService>();

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            //Business
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandler>();
            app.UseMiddleware<JwtMiddleware>();

            app.UseCors(
                options => options
                .WithOrigins(Configuration["AllowOrigins"].Split(","))
                .AllowAnyOrigin()
                .WithMethods("POST", "GET", "PUT", "DELETE")
                .AllowAnyHeader()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}