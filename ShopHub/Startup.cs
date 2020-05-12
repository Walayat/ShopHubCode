using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopHub.Models.Context;
using ShopHub.Services.AutoMapper;
using ShopHub.Services.Interface;
using ShopHub.Services.Services;

namespace ShopHub
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
            /*For dependency Injection we register our service
              and respective service interface in this method
              we define the scop of service to the interface.
             */
            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISessionManager, SessionManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILocation, LocationService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            /*AddTransient initiate our context instance everytime 
              when request come to our application.
             */
            services.AddTransient<ShopHubContext>();
            /*As user login to the application this expression creates an
             cookies in our browser with the name .ShopHub.Session and
             also define the IdleTimeout and session active age to specific period
             */
            services.AddSession(options =>
            {
                options.Cookie.Name = ".ShopHub.Session";
                options.IdleTimeout = TimeSpan.FromDays(10);
                options.Cookie.IsEssential = true;
                options.Cookie.MaxAge = TimeSpan.FromDays(10);
            });

            /*This expression is allow us to do autoMapping*/
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });

            /*This expression is also to configure autoMapping*/
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            /*This expression is allow us to runtime compilation as 
              we do any change on view we can refresh our page and changes will appear*/
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            /*This expression is allow us to work with json properly without looping nested classes*/
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // This method is actually use to activte middlewares request pipelines to our applications 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*Here I have done the globle exception handling 
              this will check the environment of our application
              from launchSetting.json file and throw specifc error
             */
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //This pipeline is use for redirection to pages
            app.UseHttpsRedirection();
            //This pipeline allow us to work with static files
            app.UseStaticFiles();

            //This pipeline allow us to work with routing in application
            app.UseRouting();

            //This pipeline allow us to work with sessions in application
            app.UseSession();
            //This pipeline allow us to do authorization in application
            app.UseAuthorization();

            //This is the default url for our application

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=AuthUser}/{action=Login}/{id?}");
            });
        }
    }
}
