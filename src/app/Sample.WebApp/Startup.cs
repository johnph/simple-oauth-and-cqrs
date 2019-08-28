namespace Sample.WebApp
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Sample.WebApp.Data;
    using Sample.WebApp.Helpers;
    using Sample.WebApp.Middlewares;
    using Sample.WebApp.Services;
    using System;
    using System.Net.Http.Headers;

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
            services.Configure<AuthToken>(Configuration.GetSection("AuthToken"));
            services.Configure<ServiceHost>(Configuration.GetSection("ServiceHost"));

            services.AddAuthentication()
            .AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection =
                 Configuration.GetSection("Authentication:Google");

                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();
                        
            services.AddNamedHttpClientConfiguration(Configuration);
            services.AddSessionConfiguration();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddTransient<ISecurityTokenHelper, SecurityTokenHelper>();
            services.AddScoped<TokenHandlerMiddleware>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication().UseMiddleware<TokenHandlerMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    internal static class ServiceExtension
    {
        public static IServiceCollection AddNamedHttpClientConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddNamedHttpClient(configuration, () => new HttpConfigAttribs()
            {
                Name = NamedHttpClients.EmployeeServiceClient,
                BaseUrl = configuration.GetSection("ServiceHost")["ResourceServer"],
                MediaType = Constants.MediaTypeAppJson
            });
           
            return services;
        }

        public static IServiceCollection AddNamedHttpClient(this IServiceCollection services, IConfiguration config, Func<HttpConfigAttribs> action)
        {
            var httpConfig = action();
            services.AddHttpClient(httpConfig.Name, client =>
            {
                client.BaseAddress = new Uri(httpConfig.BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(httpConfig.MediaType));
            });

            return services;
        }

        public static IServiceCollection AddSessionConfiguration(this IServiceCollection services)
        {
            services.AddTransient(typeof(ISession), serviceProvider =>
            {
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                return httpContextAccessor.HttpContext.Session;
            });
            services.AddSession();

            return services;
        }
    }
}
