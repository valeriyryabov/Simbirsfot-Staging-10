using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "binary(64)")]
        public string PasswordHash { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        public List<Card> CardList { get; set; }

        public List<UserEquipmentItem> UserEquipmentList { get; set; }
    }
}
