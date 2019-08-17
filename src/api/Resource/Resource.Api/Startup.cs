namespace Resource.Api
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Autofac.Extras.CommonServiceLocator;
    using CommonServiceLocator;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Resource.Api.Middlewares;
    using Resource.Infrastructure;
    using Resource.Infrastructure.Modules;
    using Swashbuckle.AspNetCore.Swagger;
    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = Configuration.GetValue<string>("AuthServer");
                o.Audience = "resourceapi";
                o.RequireHttpsMetadata = false;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiExecute", policy => policy.RequireClaim("scope", "api.execute"));
            });

            services.AddScoped<ExceptionHandlerMiddleware>();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "Simple CQRS Sample", Version = "v1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return CreateAutofacServiceProvider(services);
        }

        private IServiceProvider CreateAutofacServiceProvider(IServiceCollection services)
        {
            var container = new ContainerBuilder();

            container.Populate(services);

            container.RegisterModule(new InfrastructureModule(Configuration.GetConnectionString("DefaultConnection")));
            container.RegisterModule(new MediatorModule());

            container.Register(c =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>();
                dbContextOptionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

                return new EmployeeDbContext(dbContextOptionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();

            var buildContainer = container.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(buildContainer));

            return new AutofacServiceProvider(buildContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple CQRS Sample v1");
            });

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
