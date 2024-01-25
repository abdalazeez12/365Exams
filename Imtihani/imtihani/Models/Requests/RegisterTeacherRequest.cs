using System;
using IbtecarBusiness.Security.Interfaces;

namespace Imtihani.Models.Requests
{
    public class RegisterTeacherRequest
    {
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public bool IsWorking { get; set; }
        public string Country { get; set; }
        public string Experience { get; set; }
        public string YearsOfExperience { get; set; }
    }
}
