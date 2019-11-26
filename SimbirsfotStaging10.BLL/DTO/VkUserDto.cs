using Newtonsoft.Json;

namespace SimbirsfotStaging10.BLL.DTO
{
    public class VkUserDto
    {
        [JsonProperty("first_name")]
        public string Name { get; set; }

        [JsonProperty("last_name")]
        public string Surname { get; set; }
    }
}
