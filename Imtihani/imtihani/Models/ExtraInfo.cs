using IbtecarBusiness.Security.DataContracts;
using IbtecarBusiness.Security.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Imtihani.Models
{
    public class ExtraInfos: IbtecarBusiness.Security.Models.User
    {
        public string Username { get; set; }=string.Empty;
        public string UserInfo { get; set; } = string.Empty;
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Grade { get; set;}
        public int Roles { get; set; }
        public int Mobile { get; set; }
        public int PasswordHash { get; set; } 
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AliasName { get; set; } = string.Empty;
        public string ExtraInfo { get; set; }
        public string Permissions { get; set; } = string.Empty;
        public string SecurityStamp { get; set; } = string.Empty;
        public string Nationality { get; set;}


    }

}
