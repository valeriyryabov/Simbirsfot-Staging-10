using Newtonsoft.Json;

namespace SimbirsfotStaging10.BLL.DTO
{
    public class VkUserAuthDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
