using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AutoMapper;
using CentralDeErrosApi.Interfaces;
using CentralDeErrosApi.Infrastrutura;
using CentralDeErrosApi.Infrastrutura.CustomFilters;
using CentralDeErrosApi.Models.DTOs;
using CentralDeErrosApi.Service;
using CentralDeErrosApi.Data;

namespace CentralDeErrosApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(x => x.UseSqlServer(Configuration.GetConnectionString("CentralDeErros")));
            
            ConfigureScopes(services);
            ConfigureToken(services, Configuration);
            ConfigureSwagger(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseRouting();

            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Projeto Final");
            });
        }

        public void ConfigureScopes(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddScoped<IEnvironment, EnvironmentService>();
            services.AddScoped<ILogErrorOccurrence, LogErrorOccrurence>();
            services.AddScoped<ILevel, LevelService>();
            services.AddScoped<ISituation, SituationService>();
            services.AddScoped<IUser, UserManagementService>();
        }

        private static void ConfigureToken(IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettingsDTO>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettingsDTO>();

            var authenticationSection = configuration.GetSection("Authentication");
            services.Configure<AuthenticationDTO>(authenticationSection);
            var authentication = authenticationSection.Get<AuthenticationDTO>();

            var key = Encoding.ASCII.GetBytes(authentication.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = appSettings.Audiencia,
                    ValidIssuer = appSettings.Emissor,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };

                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Token inválido..:. " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Toekn válido...: " + context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });
        }

        public void ConfigureSwagger(IServiceCollection services)
        {
            string applicationPath = PlatformServices.Default.Application.ApplicationBasePath;
            string applicationName = PlatformServices.Default.Application.ApplicationName;
            string xmlPathDoc = Path.Combine(applicationPath, $"{applicationName}.xml");

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc(
               "v1",
               new OpenApiInfo
               {
                   Title = "Projeto Final Acelera Dev C#",
                   Version = "v1",
                   Description = "Api da central de erros",
                   Contact = new OpenApiContact
                   {
                       Name = "Squad-Acelera-Dev-Becomex",
                       Email = "marisemfs@gmail.com"
                   }
               });

                swagger.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });

                swagger.OperationFilter<AuthenticationRequirementsOperationFilter>();
                swagger.IncludeXmlComments(xmlPathDoc);
            });
        }
    }
}