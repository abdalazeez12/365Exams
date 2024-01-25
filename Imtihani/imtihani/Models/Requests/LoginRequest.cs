using IbtecarBusiness.Security.Interfaces;

namespace Imtihani.Models.Requests
{
    public class LoginRequest : ILoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
