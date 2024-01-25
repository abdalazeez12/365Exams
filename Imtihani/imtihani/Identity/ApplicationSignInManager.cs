//using Imtihani.Models;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Owin;
//using Microsoft.Owin.Security;
//using System;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace Imtihani.Identity
//{
//    public class ApplicationSignInManager : SignInManager<ApplicationUser>
//    {
//        public ApplicationSignInManager(UserManager<ApplicationUser> userManager, IAuthenticationManager authenticationManager)
//            : base(userManager, authenticationManager)
//        {
//        }

//        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
//        {
//            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
//        }

//        public static SignInManager<ApplicationUser> Create(IdentityFactoryOptions<SignInManager<ApplicationUser>> options, IOwinContext context)
//        {
//            return new ApplicationSignInManager(context.GetUserManager<UserManager<ApplicationUser>>(), context.Authentication);
//        }
//    }
//}
