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
            services.AddControllers();
            //jorge incluido
            services.AddCors();

            //jorge incluido
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
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
                /*  swagger.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                 {
                     Type = SecuritySchemeType.Http,
                     BearerFormat = "JWT",
                     In = ParameterLocation.Header,
                     Scheme = "bearer"
                 });*/
                /*swagger.OperationFilter<AuthenticationRequirementsOperationFilter>();
                string applicationPath =
                PlatformServices.Default.Application.ApplicationBasePath;
                string applicationName =
                PlatformServices.Default.Application.ApplicationName;
                string xmlPathDoc =
                Path.Combine(applicationPath, $"{applicationName}.xml");
                swagger.IncludeXmlComments(xmlPathDoc);*/

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Projeto Final");
            });

        }
    }
}