using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Entities
{
    [Table("UserEquipment")]
    public class UserEquipmentItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int EquipmentId { get; set; }
        
        [Column(TypeName ="datetime")]
        public DateTime DateBegin { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DateEnd { get; set; }

        public User User { get; set; }

        public Equipment Equipment { get; set; }
    }
}
