using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using CentralDeErrosApi.Infrastrutura;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using CentralDeErrosApi.Interfaces;
using CentralDeErrosApi.Service;
using CentralDeErrosApi.Infrastrutura.CustomFilters;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using System.Reflection;
using System;
using Microsoft.EntityFrameworkCore;
using CentralDeErrosApi.DTO;
using CentralDeErrosApi.Models;
using Microsoft.AspNetCore.Authorization;
using Pratica_Aula4_Ebtity.Models.DTOs;
using System.Threading.Tasks;

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

            services.AddDbContext<ApplicationContext>();
            services.AddScoped<IEnvironment, EnvironmentService>();
            services.AddControllers();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddScoped<ILogErrorOccurrence, LogErrorOccrurence>();
            services.AddScoped<ILevel, LevelService>();
            services.AddScoped<ISituation, SituationService>();
            services.AddScoped<IUser, UserService>();
            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CentralDeErros")));
            //jorge incluido
            services.AddCors();
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<Users>(appSettingsSection);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "macoratti.net",
                    ValidAudience = "macoratti.net",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Secret"]))
                };

                options.Events = new JwtBearerEvents
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
                string applicationPath =
                PlatformServices.Default.Application.ApplicationBasePath;
                string applicationName =
                PlatformServices.Default.Application.ApplicationName;
                string xmlPathDoc =
                Path.Combine(applicationPath, $"{applicationName}.xml");
                swagger.IncludeXmlComments(xmlPathDoc);

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });    
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // jorge incluido
            app.UseRouting();

            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

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
    }
}