using System.IO;
using SimbirsfotStaging10.BLL.Infrastructure;
using System.Net;
using System.Threading.Tasks;

namespace SimbirsfotStaging10.BLL.VK
{
    internal class VkApiMethod
    {
        private string reqUrl;


        public VkApiMethod(string methodName, string accessToken, string apiV = VkAuth.ActualApiVer, params (string, string)[] keyVal)
        {
            reqUrl = UrlMakeUtils.HttpsPrefix + $"api.vk.com/method/{methodName}?{UrlMakeUtils.UriParamsFromTuppleArr(keyVal)}&access_token={accessToken}&v={apiV}";
        }


        public async Task<string> GetJsonRespAsync()
        {
            var resp = await WebRequest.CreateHttp(reqUrl).GetResponseAsync();
            return new StreamReader(resp.GetResponseStream()).ReadToEnd();
        }
    }

    internal static class VkApiMethods
    {
        public const string GetProfileInfo = "users.get";
    }
}
