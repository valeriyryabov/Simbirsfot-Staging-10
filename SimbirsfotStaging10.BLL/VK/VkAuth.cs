using System;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Infrastructure;
using SimbirsfotStaging10.BLL.Interfaces;

namespace SimbirsfotStaging10.BLL.VK
{
    public class VkAuth : IAuthService
    {
        private const string AuthServerUrl = UrlMakeUtils.HttpsPrefix + "oauth.vk.com";
        private const string AuthorizePost = "/authorize";
        private const string AccessTokenPost = "/access_token";
        private const string GrantType = "code";
        private const string RedirectUrl = UrlMakeUtils.HttpsPrefix + "localhost:44374/account/authorize?service=Vk";
        public const string ActualApiVer = "5.103";

        private string clientSecret;
        private string clientId;


        public VkAuth(IConfiguration conf)
        {
            SetClientCredentials(conf);
        }


        public string UrlGetCode => $"{AuthServerUrl}{AuthorizePost}?client_id={clientId}&display=page&redirect_uri={RedirectUrl}"
                                           + $"&response_type={GrantType}&v={ActualApiVer}";


        private string UrlGetAccessToken(string code) => $"{AuthServerUrl}{AccessTokenPost}?client_id={clientId}&client_secret={clientSecret}" +
                                    $"&redirect_uri={RedirectUrl}&code={code}";


        private void SetClientCredentials(IConfiguration conf)
        {
            var credentials = conf.GetSection("VkApplicationCredentials").GetChildren().ToDictionary(sec => sec.Key, sec => sec.Value);
            clientId = credentials["ClientId"];
            clientSecret = credentials["ClientSecret"];
        }

        public async Task<IAuthServUserApi> Authorize(string code, HttpContext httpContext)
        {
            var resp = await WebRequest.Create(UrlGetAccessToken(code)).GetResponseAsync();
            var json = new StreamReader(resp.GetResponseStream()).ReadToEnd();
            var userAuthDto = JsonConvert.DeserializeObject<VkUserAuthDto>(json);
            var claimsIdentity = new ClaimsIdentity("Cookie");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Authentication, userAuthDto.AccessToken));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, userAuthDto.UserId));
            var vkApi = new VkUserApi(userAuthDto);
            var user = await vkApi.GetUserProfile();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user?.Name));
            await httpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(claimsIdentity));
            return vkApi;
        }
    }
}
