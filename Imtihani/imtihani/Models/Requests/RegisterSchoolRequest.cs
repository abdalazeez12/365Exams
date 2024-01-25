using System;
using IbtecarBusiness.Security.Interfaces;

namespace Imtihani.Models.Requests
{
    public class RegisterSchoolRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Level { get; set; }
    }
}
