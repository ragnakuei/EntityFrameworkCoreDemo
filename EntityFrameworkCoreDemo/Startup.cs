using EntityFrameworkCoreDemo.BLL;
using EntityFrameworkCoreDemo.Controllers;
using EntityFrameworkCoreDemo.DAL;
using EntityFrameworkCoreDemo.EF;
using EntityFrameworkCoreDemo.IBLL;
using EntityFrameworkCoreDemo.IDAL;
using EntityFrameworkCoreDemo.Models.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace EntityFrameworkCoreDemo
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
            services.Configure<CookiePolicyOptions>(options =>
                                                    {
                                                        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                                                        options.CheckConsentNeeded    = context => true;
                                                        options.MinimumSameSitePolicy = SameSiteMode.None;
                                                    });

            services.AddDbContext<DemoDbContext>(options => options
                                                            .UseSqlServer(Configuration.GetConnectionString("DemoDb"))
                                                            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddTransient<HomeController>();
            services.AddTransient<CountryController>();
            services.AddTransient<CountyController>();
            services.AddTransient<CvController>();
            services.AddTransient<ICountryBLL, CountryBLL>();
            services.AddTransient<ICountyBLL, CountyBLL>();
            services.AddTransient<ICvBLL, CvBLL>();
            services.AddTransient<ICountryDAL, CountryDAL>();
            services.AddTransient<ICountyDAL, CountyDAL>();
            services.AddTransient<ICvDAL, CvDAL>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<UserInfo, UserInfo>();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Trace));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
                       {
                           routes.MapRoute(
                                           "default",
                                           "{controller=Home}/{action=Index}/{id?}");
                       });
        }
    }
}