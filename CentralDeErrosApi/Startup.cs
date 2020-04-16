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

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
            services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });


            services.AddCors();
            var appSettingsSection = Configuration.GetSection("AppSettings");
           
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = false,
                    ValidIssuer = "acelera dev",
                    ValidAudience = "acelera dev",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Secret"])),
                    ClockSkew = TimeSpan.Zero,
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