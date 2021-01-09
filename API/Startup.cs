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
using Resources.Authorization;
using AutoMapper;
using Resources.DTO;
using Service.Mapping;
using Microsoft.Extensions.FileProviders;
using System.IO;

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
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
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
                opt.AddPolicy("Get", policy =>
                {
                    policy.Requirements.Add(new GetRequirement());
                });
                opt.AddPolicy("Post", policy =>
                {
                    policy.Requirements.Add(new PostRequirement());
                });
                opt.AddPolicy("Put", policy =>
                {
                    policy.Requirements.Add(new PutRequirement());
                });
                opt.AddPolicy("Delete", policy =>
                {
                    policy.Requirements.Add(new DeleteRequirement());
                });
                opt.AddPolicy("AccessProject", policy =>
                {
                    policy.Requirements.Add(new ProjectRequirement());
                });
            });

            services.AddTransient<IAuthorizationHandler, GetRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, PostRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, PutRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, DeleteRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, ProjectRequirementHandler>();

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
            services.AddScoped<IPhaseService, PhaseService>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOptionService, OptionService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IProfile, Repository.Profile>();
            services.AddScoped<IRole, Role>();
            services.AddScoped<IPhase, Phase>();
            services.AddScoped<ICatalog, Catalog>();
            services.AddScoped<IUser, User>();
            services.AddScoped<IOption, Option>();
            services.AddScoped<IClient, Client>();
            services.AddScoped<IProject, Project>();
            services.AddScoped<IActivity, Activity>();

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BaseProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new RoleProfile(provider.GetService<IBaseService>()));
                cfg.AddProfile(new CatalogProfile(provider.GetService<IBaseService>()));
                cfg.AddProfile(new PhaseProfile(provider.GetService<IBaseService>()));
                cfg.AddProfile(new UserProfile(provider.GetService<IBaseService>()));
                cfg.AddProfile(new OptionProfile(provider.GetService<IBaseService>()));
                cfg.AddProfile(new ClientProfile(provider.GetService<IBaseService>()));
                cfg.AddProfile(new ProjectProfile(provider.GetService<IBaseService>()));
                cfg.AddProfile(new ActivityProfile(provider.GetService<IBaseService>()));
                cfg.AddProfile(new SelectedProfile(provider.GetService<IBaseService>()));
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

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Files")),
                RequestPath = "/Files",
                OnPrepareResponse = (context) =>
                {
                    context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                    context.Context.Response.Headers["Pragma"] = "no-cache";
                    context.Context.Response.Headers["Expires"] = "-1";
                }
            });

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
