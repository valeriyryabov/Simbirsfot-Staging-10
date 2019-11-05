using System.Threading.Tasks;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Interfaces;
using SimbirsfotStaging10.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Quartz;


namespace SimbirsfotStaging10.BLL.Services
{

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UserService> _logger;


        public UserService(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<UserService> logger)
        {
            _userManager = UserManager;
            _signInManager  = SignInManager;
            _logger = logger;
        }


        async public Task<IdentityResult> Create(UserRegisterDTO userDTO)
        {
            _logger.LogInformation("Creat");
            return await _userManager.CreateAsync(CreateUserEntityFromDto(userDTO), userDTO.PasswordHash);
        }




        public async Task<SignInResult> SignIn(UserRegisterDTO userDTO)
        {
            _logger.LogInformation("SignIn");
            return await _signInManager.PasswordSignInAsync(userDTO.UserName, userDTO.PasswordHash, false, false);
        }



        async public Task LogOut()
        {            
            await _signInManager.SignOutAsync();
            _logger.LogInformation("SignOut");
        } 


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
