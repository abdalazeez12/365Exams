using IbtecarBusiness.Security.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Imtihani.Models
{

    public class ApplicationUser
    {
        private readonly IHttpContextAccessor _context;

        public ApplicationUser(IHttpContextAccessor context)
        {
            _context = context;
        }
        [Required]
        public Guid Id
        {
            get;
            set;
        }
        [Required]
        public string Email
        {
            get;
            set;
        }
        public string Mobile
        {
            get;
            set;
        }
        public string Gender
        {
            get;
            set;
        }
        public string AliasName
        {
            get;
            set;
        }
        public bool IsEnabled
        {
            get;
            set;
        }
        public bool IsLocked
        {
            get;
            set;
        }
        public DateTime? LockDate
        {
            get;
            set;
        }
        public bool EmailConfirmed
        {
            get;
            set;
        }
        public bool MobileConfirmed
        {
            get;
            set;
        }
        [DefaultValue(2)]
        public int UserType
        {
            get;
            set;
        } = 2;
        public string SecurityStamp
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public DateTime? BirthDate
        {
            get;
            set;
        }
        public bool IsSubscribedToNewsLetter
        {
            get;
            set;
        }
        public bool IsDeletable
        {
            get;
            set;
        }
        public DateTime? JoinDate
        {
            get;
            set;
        }
        public string ExtraInfo
        {
            get;
            set;
        }
        public string PasswordHash
        {
            get;
            set;
        }
        public List<int> Roles { get; set; }
        public List<int> Permissions { get; set; }

        public ApplicationUser(User user)
        {
            Address = user.Address;
            AliasName = user.AliasName;
            BirthDate = user.BirthDate;
            Email = user.Email;
            EmailConfirmed = user.EmailConfirmed;
            Id = user.Id;
            IsDeletable = user.IsDeletable;
            IsEnabled = user.IsEnabled;
            IsLocked = user.IsLocked;
            IsSubscribedToNewsLetter = user.IsSubscribedToNewsLetter;
            JoinDate = user.JoinDate;
            LockDate = user.LockDate;
            Mobile = user.Mobile;
            MobileConfirmed = user.MobileConfirmed;
            PasswordHash = user.PasswordHash;
            SecurityStamp = user.SecurityStamp;
            //base.TwoFactorEnabled = user.TwoFactorEnabled;
            //base.UserName = user.UserName;
            UserType = user.UserType;
            ExtraInfo = user.ExtraInfo;
            UserName = user.Email;
            Roles = user.Roles;
            Permissions = user.Permissions;
        }

        public string UserName
        {
            get;
            set;
        }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync()
        //{
        //    this.UserName = this.Email;
        //    this.IsEnabled = true;
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    //var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    var userIdentity = (ClaimsIdentity)_context.HttpContext.User.Identity;

        //    userIdentity.AddClaim(new Claim(ClaimTypes.Sid, this.Id.ToString()));
        //    userIdentity.AddClaim(new Claim(ClaimTypes.Email, this.Email));
        //    userIdentity.AddClaim(new Claim("UserType", this.UserType.ToString()));
        //    userIdentity.AddClaim(new Claim(ClaimTypes.Name, this.AliasName ?? "Guest"));
        //    userIdentity.AddClaim(new Claim(ClaimTypes.Role, string.Join(",", this.Permissions)));
        //    return userIdentity;
        //}

        //public string Id
        //{
        //    get { return Id.ToString(); }
        //}
    }
}
