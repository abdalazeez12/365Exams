using IbtecarBusiness.Core.Services;
using IbtecarBusiness.Courses.Services;
using IbtecarBusiness.Security.Interfaces;
using IbtecarBusiness.Security.Services;
using IbtecarFramework;
using IbtecarFramework.Apis.Models;
using IbtecarFramework.Apis.Services;
using Imtihani.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using IbtecarBusiness.Articles;
using IbtecarBusiness.Security;
using IbtecarBusiness.Catalog;
using IbtecarBusiness.Courses;
using IbtecarBusiness.Core;




namespace Imtihani
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

            new CoreModule().AddModule(services, Configuration);
            new ArticlesModule().AddModule(services, Configuration);
            new CatalogModule().AddModule(services, Configuration);
            new CoursesModule().AddModule(services, Configuration);
            new SecurityModule().AddModule(services, Configuration);
            services.AddMixedTokenServices(Configuration);

            //services.AddScoped<EmailService>();
            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();
            services.AddSession();

            //services.AddScoped<IAuthService, AuthService>();

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            //services.AddIdentity<IdentityUser, IdentityRole>()
            //        .AddEntityFrameworkStores<ApplicationUser>()
            //        .AddSignInManager<SignInManager<IdentityUser>>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IPolicy, BaseApiPolicy>();
            services.AddScoped<StaticPageService>();
            services.AddControllersWithViews();
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "346124578302792";
                options.AppSecret = "ec83d2278052bd0dac7b41ec9be11d55";
            });
            services.AddRazorPages();
            services.AddHttpClient();
            //services.AddJWTTokenServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            var supportedCultures = new[] { "en", "ar" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();



            app.UseAuthorization();
            app.UseAuthentication();
            //app.UseMvc();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Home",
                    "{action}/{id?}",
                    defaults: new { controller = "Home", action = "index" });
                endpoints.MapRazorPages();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Account",
                    "{controller=Account}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });


        }
    }
}
