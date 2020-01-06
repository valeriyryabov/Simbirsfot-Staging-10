using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimbirsfotStaging10.DAL.Entities
{
    [Table("Equipment")]
    public class Equipment
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Type { get; set; }

        public bool IsDeleted { get; set; }

        public List<UserEquipmentItem> UserEquipmentList { get; set; }

        
        public Equipment()
        {
            UserEquipmentList = new List<UserEquipmentItem>();
        }
    }
}
