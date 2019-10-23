using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimbirsfotStaging10.BLL.DTO
{
    public class UserLoginDTO
    {
        [Required]
        public string EmailOrUserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
   
        public bool RememberMe { get; set; }
    }
}
