using System.Threading.Tasks;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Interfaces;
using SimbirsfotStaging10.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Quartz;
using SimbirsfotStaging10.Logger;
using Microsoft.AspNetCore.Http;

namespace SimbirsfotStaging10.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UserService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<User> UserManager, SignInManager<User> SignInManager, 
            ILogger<UserService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = UserManager;
            _signInManager = SignInManager;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        async public Task<IdentityResult> Create(UserRegisterDTO userDTO)
        {
            var userCreate = await _userManager.CreateAsync(CreateUserEntityFromDto(userDTO), userDTO.PasswordHash);
            LogUserOperationInfo($"User was{(userCreate.Succeeded ? "" : "n't")} created.",
                (int)(userCreate.Succeeded ? EventType.RegistrationSucces : EventType.RegistrationFail),
                userDTO);
            return userCreate;
        }


        public async Task<SignInResult> SignIn(UserRegisterDTO userDTO)
        {
            var userSignIn = await _signInManager.PasswordSignInAsync(userDTO.UserName, userDTO.PasswordHash, false, false);
            LogUserOperationInfo($"User {(userSignIn.Succeeded ? "" : "failed")} signed in.",
                (int)(userSignIn.Succeeded ? EventType.SignInSucces : EventType.SignInFail),
                userDTO);
            return userSignIn;
        }



        async public Task LogOut()
        {
            var tmplForClaimId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            var claimId = _signInManager.Context.User.Claims.FirstOrDefault(claim => claim.Type == tmplForClaimId);
            if (claimId != null)
            {
                var user = await _userManager.FindByIdAsync(claimId.Value);
                if (user != null)
                    LogUserOperationInfo("User log out.",
                        (int)EventType.Logout,
                        new UserRegisterDTO
                        {
                            Email = user.Email,
                            UserName = user.UserName,
                            Name = user.Name
                        });
            }
            await _signInManager.SignOutAsync();
        }


        public async Task<SignInResult> SignInByEmailPassword(UserLoginDTO userDTO)
        {
            var resSignIn = await _signInManager.PasswordSignInAsync(userDTO.EmailOrUserName, userDTO.Password
                , userDTO.RememberMe, false);
            if (!resSignIn.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(userDTO.EmailOrUserName);
                if (user != null)
                    resSignIn = await _signInManager.PasswordSignInAsync(user, userDTO.Password, userDTO.RememberMe, false);
            }
            LogUserOperationInfo($"User {(resSignIn.Succeeded ? "" : "failed")} signed in.",
                (int)(resSignIn.Succeeded ? EventType.SignInSucces : EventType.SignInFail),
                userDTO);
            return resSignIn;
        }


        static User CreateUserEntityFromDto(UserRegisterDTO userDTO)
        {
            var userEntity = new User
            {
                Email = userDTO.Email,
                PasswordHash = userDTO.PasswordHash,
                Name = userDTO.Name,
                UserName = userDTO.UserName
            };
            return userEntity;
        }

        void LogUserOperationInfo(string mes, int eventId, object userDto) => _logger.LogInformation(new EventId(eventId)
            , $"{mes} {GetDtoInfo(userDto)}");


        static string GetDtoInfo(object userDto)
        {
            var type = userDto.GetType();
            var dataTypeAtr = typeof(DataTypeAttribute);
            var dataTypeProp = dataTypeAtr.GetProperty("DataType");
            var str = "";
            foreach (var prop in type.GetProperties())
            {

                var isPasswordAtr = prop.GetCustomAttributes(false).Any(atr =>
                {
                    if (atr.GetType().Name == dataTypeAtr.Name)
                        return (DataType)dataTypeProp.GetValue(atr) == DataType.Password;
                    return false;
                });
                if (isPasswordAtr)
                    continue;
                str += $"{prop.Name}: {prop.GetValue(userDto)}. ";
            }
            return str.Substring(0, str.Length - 1);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }

        public async Task<int> GetCurrentUserIDAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return user.Id;
        }
    }
}
