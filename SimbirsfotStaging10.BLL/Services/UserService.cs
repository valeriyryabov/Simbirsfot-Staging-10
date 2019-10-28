using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Interfaces;
using SimbirsfotStaging10.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using SimbirsfotStaging10.DAL.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SimbirsfotStaging10.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> UserManager, SignInManager<User> SignInManager )
        {
            _userManager = UserManager;
            _signInManager  = SignInManager;
        }


        async public Task<IdentityResult> Create(UserRegisterDTO userDTO)=>
            await _userManager.CreateAsync(CreateUserEntityFromDto(userDTO), userDTO.PasswordHash);

            

        public async Task<SignInResult> SignIn(UserRegisterDTO userDTO) => 
            await _signInManager.PasswordSignInAsync(userDTO.UserName,userDTO.PasswordHash,false,false);


        async public Task LogOut() => await _signInManager.SignOutAsync();


        public async Task<SignInResult> SignInByEmailPassword(UserLoginDTO userDTO)
        {
            var resSignIn = await _signInManager.PasswordSignInAsync(userDTO.EmailOrUserName, userDTO.Password
                , userDTO.RememberMe, false);
            if (!resSignIn.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(userDTO.EmailOrUserName);
                if(user != null)
                    resSignIn = await _signInManager.PasswordSignInAsync(user, userDTO.Password, userDTO.RememberMe, false);
            }                     
            return resSignIn;
        }


        static User CreateUserEntityFromDto(UserRegisterDTO userDTO)
        {
            var userEntity = new User {
                Email = userDTO.Email,
                PasswordHash = userDTO.PasswordHash,
                Name = userDTO.Name,
                UserName = userDTO.UserName
            };
            return userEntity;
        }


        public void Dispose()
        {
            _userManager.Dispose();
        }       
    }
}
