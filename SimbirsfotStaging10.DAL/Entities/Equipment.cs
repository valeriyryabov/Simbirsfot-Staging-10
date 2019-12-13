using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimbirsfotStaging10.DAL.Entities
{
    [Table("Equipment")]
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<UserEquipmentItem> UserEquipmentList { get; set; }
    }
}
