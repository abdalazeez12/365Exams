////using IbtecarBusiness.Security.DataContracts;
////using Microsoft.AspNetCore.Identity;
////using System;
////using System.Security.Claims;
////using System.Threading.Tasks;

////namespace Imtihani.Identity
////{
////    public class ApplicationUser : User//, IUser<Guid>
////    {
////        public ApplicationUser()
////            : base()
////        {
////        }

////        public ApplicationUser(User user)
////        {
////            base.Address = user.Address;
////            base.AliasName = user.AliasName;
////            base.BirthDate = user.BirthDate;
////            base.Email = user.Email;
////            base.EmailConfirmed = user.EmailConfirmed;
////            base.Id = user.Id;
////            base.IsDeletable = user.IsDeletable;
////            base.IsEnabled = user.IsEnabled;
////            base.IsLocked = user.IsLocked;
////            base.IsSubscribedToNewsLetter = user.IsSubscribedToNewsLetter;
////            base.JoinDate = user.JoinDate;
////            base.LockDate = user.LockDate;
////            base.Mobile = user.Mobile;
////            base.MobileConfirmed = user.MobileConfirmed;
////            base.PasswordHash = user.PasswordHash;
////            base.SecurityStamp = user.SecurityStamp;
////            base.TwoFactorEnabled = user.TwoFactorEnabled;
////            base.UserName = user.UserName;
////            base.UserType = user.UserType;
////            base.ExtraInfo = user.ExtraInfo;
////            this.UserName = user.Email;
////            this.Roles = user.Roles;
////            this.Permissions = user.Permissions;
////        }

////        public string UserName
////        {
////            get;
////            set;
////        }

////        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
////        {
////            this.UserName = this.Email;
////            this.IsEnabled = true;
////            Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
////           var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
////            userIdentity.AddClaim(new Claim(ClaimTypes.Sid, this.Id.ToString()));
////            userIdentity.AddClaim(new Claim(ClaimTypes.Email, this.Email));
////            userIdentity.AddClaim(new Claim("UserType", this.UserType.ToString()));
////            userIdentity.AddClaim(new Claim(ClaimTypes.Name, this.AliasName ?? "Guest"));
////            userIdentity.AddClaim(new Claim(ClaimTypes.Role, string.Join(",", this.Permissions)));
////            return userIdentity;
////        }

////        public string ID
////        {
////            get { return Id.ToString(); }
////        }
////    }
////}
