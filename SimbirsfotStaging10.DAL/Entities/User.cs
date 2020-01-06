using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimbirsfotStaging10.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        [Column(TypeName = "varchar(255)")]
        public override string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public override string PasswordHash { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        public List<Card> CardList { get; set; }

        public List<UserEquipmentItem> UserEquipmentList { get; set; } 
        

        public User()
        {
            CardList = new List<Card>();
            UserEquipmentList = new List<UserEquipmentItem>();
        }
    }
}
