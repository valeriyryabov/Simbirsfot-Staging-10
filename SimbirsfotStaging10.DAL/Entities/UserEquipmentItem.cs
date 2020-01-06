using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace SimbirsfotStaging10.DAL.Entities
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


        public UserEquipmentItem()
        {
            User = new User();
        }
    }
}
