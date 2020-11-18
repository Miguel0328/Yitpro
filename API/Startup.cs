using System;
using System.Text;
using API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Repository;
using Repository.Interfaces;
using Service;
using Service.Interfaces;
using Service.Permission;
using AutoMapper;
using Service.DTO;
using Service.Mapping;

namespace API
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
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("WWW-Authenticate")
                    .WithOrigins("http://localhost:3000")
                    .AllowCredentials();
                });
            });

            services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<ProfileDTO>();
                    //cfg.ValidatorOptions.LanguageManager.Culture = new CultureInfo("en");
                });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Create", policy =>
                {
                    policy.Requirements.Add(new CreateRequirement());
                });
                opt.AddPolicy("View", policy =>
                {
                    policy.Requirements.Add(new ViewRequirement());
                });
            });

            services.AddTransient<IAuthorizationHandler, CreateRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, ViewRequirementHandler>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IProfile, Repository.Profile>();
            services.AddScoped<IRole, Role>();

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RoleProfile(provider.GetService<IBaseService>()));
            }).CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
