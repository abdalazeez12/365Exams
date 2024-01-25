using IbtecarBusiness.Security.Interfaces;
using System.Threading.Tasks;

namespace Imtihani.Interfaces
{
    public interface IAuthService
    {
        //Task<IAuthResponse> RegisterAsync(IbtecarBusiness.Security.DataContracts.User user);

        Task<IAuthResponse> LoginAsync(string email, string password);
    }
}
