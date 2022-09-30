using PayrollServer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.IO;
using Entities.Enumerations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Entities.Models;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Contracts;
using Repository;

namespace PayrollServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();

            services.ConfigureIISIntegration();

            services.ConfigureLoggerService();

            services.ConfigureMySqlContext(Configuration);

            services.ConfigureRepositoryWrapper();
            services.AddScoped<IUserRepository, UserRepository>();

            services.ConfigureSynergyContext();

            services.ConfigureSwagger();

            services.ConfigureAutoMapper();

            services.AddMvc();

            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


            

            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<RepositoryContext>();

            services.Configure<IdentityOptions>(options =>
            {
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequireUppercase = true;
                //options.Password.RequiredLength = 6;
                //options.Password.RequiredUniqueChars = 0;

            });

            services.AddSession(options =>
            {
                options.Cookie.Name = "UserSession";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddCookie()
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // Put a breakpoint here
                        return Task.CompletedTask;
                    },
                };

                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidAudience = Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                                                       (Configuration["Token:Key"]))
                };
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options => Configuration.Bind("CookieSettings", options)); ;



            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("ViewPersonsList", policy => policy.RequireAssertion(con => con.User.HasClaim(c =>
                c.Type == UserClaimTypeEnum.Admin || c.Type == UserClaimTypeEnum.Operator )));

                opt.AddPolicy("AddEditPerson", policy => policy.RequireAssertion(con => con.User.HasClaim(c =>
                c.Type == UserClaimTypeEnum.Admin || c.Type == UserClaimTypeEnum.Operator)));
               
                opt.AddPolicy("AddEditViewUsers", policy => policy.RequireAssertion(con =>
                con.User.HasClaim(c => c.Type == UserClaimTypeEnum.Admin)));

                
            });

           

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(6);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.Use(async (context, next) =>
                {
                    await next();
                    if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                    {
                        context.Request.Path = "/index.html";
                        await next();
                    }
                });
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseStaticFiles();

            //app.Use(async (context, next) =>
            //{
            //    var JWToken = context.Session.GetString("UserSession");
            //    if (!string.IsNullOrEmpty(JWToken))
            //    {
            //        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //    }
            //    await next();
            //});

            app.UseSession();


            app.UseAuthentication();
            app.UseAuthorization();


            //app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Main}/{action=Index}");
            });
        }
    }
}
