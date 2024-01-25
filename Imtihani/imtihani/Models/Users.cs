using System;

namespace Imtihani.Models
{
    public class Users
    {
        public string UserName
        {
            get;
            set;
        }
        public Guid Id
        {
            get;
            set;
        }
        public string EmailId
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
    }
    public class UserDto
    {
        public Guid Id { get; set; }
        public Guid GuidId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Validity { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}