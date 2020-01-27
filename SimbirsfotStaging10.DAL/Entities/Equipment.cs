using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimbirsfotStaging10.DAL.Entities
{
    [Table("Equipment")]
    public class Equipment
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public int Price { get; set; }

        public int Length { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string ModelName { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Gender { get; set; }

        public float Size { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Description { get; set; }


        public int EquipmentTypeId { get; set; }

        [ForeignKey("EquipmentTypeId")]
        public EquipmentType EquipmentTypeItem { get; set; }


        public List<UserEquipmentItem> UserEquipmentList { get; set; }

        public Equipment()
        {
            UserEquipmentList = new List<UserEquipmentItem>();
        }
    }
}
