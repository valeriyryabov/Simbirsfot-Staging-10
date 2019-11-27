using System;
using SimbirsfotStaging10.BLL.DTO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;


namespace SimbirsfotStaging10.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<IdentityResult> Create(UserRegisterDTO userDTO);
        Task<SignInResult> SignInByEmailPassword(UserLoginDTO userDTO);
        Task LogOut();
        Task<SignInResult> SignIn(UserRegisterDTO userDTO);
    }
}
