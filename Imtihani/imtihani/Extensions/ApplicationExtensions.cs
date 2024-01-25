using IbtecarBusiness.Security;
using IbtecarBusiness.Security.DataContracts;
using IbtecarBusiness.Security.Services;
using IbtecarBusiness.Security.Interfaces;
//using Imtihani.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Imtihani.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IAuthService, AuthService>();
            /*services.AddIdentity<ApplicationUser, ApplicationRole>(o => {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = true;
                o.User.RequireUniqueEmail = true;
            });//.AddEntityFrameworkStores(identityStore).Adddefaul*/

            /*services.AddDbContext<SecurityContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("SecurityContext"))
                );*/
            return services;
        }

    }
}
