using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Entities;
using WebAPItwe.InRepositories;
using WebAPItwe.Repositories;
using WebAPItwe.Services;
using CorePush.Google;
using CorePush.Apple;
using WebAPItwe.Setting;

namespace WebAPItwe
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
            services.AddScoped<InMentorRepository, MentorRepository>();
            services.AddScoped<InUserRepository, UserRepository>();
            services.AddScoped<InMemberRepository, MemberRepository>();
            services.AddScoped<InSessionRepository, SessionRepository>();
            services.AddScoped<InMemberSessionRepository, MemberSessionRepository>();
            services.AddScoped<InNotificationRepository, NotificationRepository>();
            services.AddControllers();
            services.AddDbContext<dbEWTContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnectionString")));

            //notification 
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<INotificationService2, NotificationService2>();
            services.AddHttpClient<FcmSender>();
            services.AddHttpClient<ApnSender>();
            // Configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("FcmNotification");
            services.Configure<FcmNotificationSetting>(appSettingsSection);

            // Register the swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "The weekend expertise Web API",
                    Description = "Document for Web API",
                    //TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Dươngas",
                        Email = string.Empty,
                        Url = new Uri("https://www.facebook.com/duong.as.35/"),
                    }                 
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            // add cors
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TWE Web API v1");
                c.RoutePrefix = string.Empty;
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Make sure you call this before calling app.UseMvc()
            app.UseCors(options => options.AllowAnyOrigin());



            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
