using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimbirsfotStaging10.DAL.Entities
{
    public class EquipmentType
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string AgeSize { get; set; }

        public List<Equipment> EquipmentList;


        public EquipmentType()
        {
            EquipmentList = new List<Equipment>();
        }
    }
}
