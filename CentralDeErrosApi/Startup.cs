using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using CentralDeErrosApi.Infrastrutura;
using Microsoft.EntityFrameworkCore;
using CentralDeErrosApi.Infrastrutura.CustomFilters;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CentralDeErrosApi
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
            //jorge incluido
            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<ApplicationContext>(x => x.UseSqlServer(Configuration.GetConnectionString("default")));

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
                  });

                  swagger.OperationFilter<AuthenticationRequirementsOperationFilter>();

                  string applicationPath =
                  PlatformServices.Default.Application.ApplicationBasePath;

                  string applicationName =
                  PlatformServices.Default.Application.ApplicationName;

                  string xmlPathDoc =
                  Path.Combine(applicationPath, $"{applicationName}.xml");

                  swagger.IncludeXmlComments(xmlPathDoc);*/

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // jorge incluido
            app.UseRouting();            

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Projeto Final");
            });
        }
    }
}
