using System;
using IbtecarBusiness.Security.Interfaces;

namespace Imtihani.Models.Requests
{
    public class RegisterRequest : IRegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Nationality { get; set; }
        public int Grade { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
