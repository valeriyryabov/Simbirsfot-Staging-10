using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimbirsfotStaging10.BLL.DTO;

namespace SimbirsfotStaging10.BLL.VK
{
    public class VkUserApi
    {
        private VkUserAuthDto userAuthDto;

        public VkUserApi(VkUserAuthDto vkUserAuth)
        {
            userAuthDto = vkUserAuth;
        }


        public async Task<VkUserDto> GetUserProfile()
        {
            var vkMethod = new VkApiMethod(VkApiMethods.GetProfileInfo, userAuthDto.AccessToken, keyVal: ("user_ids", userAuthDto.UserId));
            var user = JsonConvert.DeserializeObject<VkUsersGetDto>(await vkMethod.GetJsonRespAsync()).Users.FirstOrDefault();
            return user;
        }
    }
}
