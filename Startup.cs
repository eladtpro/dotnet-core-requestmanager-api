using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RequestManager.Directory;
using System;
using System.Threading.Tasks;
using RequestManager.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos;
using RequestManager.Model;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace RequestManager
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
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(options =>
            {
                Configuration.Bind("OpenIdConnect", options);
                options.Events.OnRedirectToIdentityProvider = context =>
                {
                    context.Response.Headers["Location"] = context.Request.Path.Value;
                    context.Response.StatusCode = 302;
                    return Task.CompletedTask;
                };
            })
            //.AddJwtBearer(options =>
            //{
            //    Configuration.Bind("JwtBearer", options);
            //})
            .AddCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.Headers["Location"] = context.RedirectUri;
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:4200"/*, "http://localhost:8080", "http://localhost:3000"*/);
            }));

            services.AddLogging(builder => builder
                .AddConsole() // Register the logger with the ILoggerBuilder
                .AddDebug()
                .AddEventSourceLogger()
                .SetMinimumLevel(LogLevel.Debug) // Set the minimum log level to Information
            );

            services.AddSingleton<CosmosDb>(o => 
            {
                CosmosConfiguration options = new CosmosConfiguration();
                Configuration.Bind("CosmosDb", options);
                CosmosClientBuilder clientBuilder = new CosmosClientBuilder(options.Account, options.Key);
                CosmosClient client = clientBuilder.WithConnectionModeDirect().Build();
                DatabaseResponse response = client.CreateDatabaseIfNotExistsAsync(options.DatabaseName).Result;
                return new CosmosDb(response.Database);
            });

            services.AddSignalR();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // https://stackoverflow.com/questions/36641338/how-get-current-user-in-asp-net-core

            services.AddMemoryCache();
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "localhost";
                option.InstanceName = "RequestManager";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Request API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider svp, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                LdapConnectionSettings.Current = Configuration.GetSection("LdapConnection").Get<LdapConnectionSettings>();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    IExceptionHandlerFeature contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    Error error = (contextFeature != null) ?
                        new Error(contextFeature.Error) :
                        new Error(new Exception("Unknown Exception"));
                    error.Code =  context.Response.StatusCode;
                    logger.LogError(error.ToString());
                    await context.Response.WriteAsync(error.ToString());
                });
            });

            app.UseSwagger();
            app.UseAuthentication();

            // https://medium.com/@rukshandangalla/how-to-notify-your-angular-5-app-using-signalr-5e5aea2030b2
            app.UseCors("CorsPolicy");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Request Manager API V1");
                c.RoutePrefix = string.Empty;
            });


            // https://stackoverflow.com/questions/37329354/how-to-use-ihttpcontextaccessor-in-static-class-to-set-cookies
            //here is where you set you accessor
            IHttpContextAccessor accessor = svp.GetService<IHttpContextAccessor>();
            LdapRepository.SetHttpContextAccessor(accessor);

            app.UseMvc();
        }


    }
}
