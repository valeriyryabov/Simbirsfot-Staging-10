﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace SimbirsfotStaging10.BLL.DTO
{
    public class VkUsersGetDto
    {
        [JsonProperty("response")]
        public List<VkUserDto> Users { get; set; }
    }
}
