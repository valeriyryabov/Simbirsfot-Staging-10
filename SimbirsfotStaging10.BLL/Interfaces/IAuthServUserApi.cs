using System.Threading.Tasks;
using SimbirsfotStaging10.BLL.DTO;

namespace SimbirsfotStaging10.BLL.Interfaces
{
    public interface IAuthServUserApi
    {
        Task<AuthServUserDto> GetUserProfile();
    }
}
