using System.ComponentModel.DataAnnotations;

namespace SimbirsfotStaging10.BLL.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

    }
}
