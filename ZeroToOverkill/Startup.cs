using System;
using Business.Impl.Services;
using Business.Services;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZeroToOverkill.Filters;
using ZeroToOverkill.Middlewares;

namespace ZeroToOverkill
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            string value = _configuration.GetValue<string>("Logging:LogLevel:Microsoft");

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //public IServiceProvider ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc(option => option.EnableEndpointRouting = false);
        //    // services.AddBusiness();

        //    var containerBuilder = new ContainerBuilder()
        //    containerBuilder.RegisterModule<AutoFacModule>();
        //    containerBuilder.Populate(services);
        //    var container = containerBuilder.Build();
        //    return new AutofacServiceProvider(container);

        //}
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
            });
            services.AddDbContext<GroupMangmentDbContext>(
                option => { option.UseNpgsql(_configuration.GetConnectionString("GroupMangmentDbContext")); }
            );
            services.AddScoped<IGroupService, GroupService>();
            //services.AddBusiness();
        }
        //public void ConfigureContainer(ContainerBuilder builder)
        //{

        //    builder.RegisterModule<AutoFacModule>();
        //}
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMiddleware<RequestTimingAdHocMiddleware>();

            //app.UseMiddleware<RequestTimingFactoryMiddleware>();
            app.UseMvc();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("No middleware could handle the request");
            });
        }
    }
}
