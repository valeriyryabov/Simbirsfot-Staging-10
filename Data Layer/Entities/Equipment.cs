using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    [Table("Equipment")]
    public class Equipment
    {
        public int Id { get; set; }
        public int Type { get; set; }     
        public bool IsDeleted { get; set; }
        public List<UserEquipmentItem> UserEquipmentList { get; set; }
    }
}
