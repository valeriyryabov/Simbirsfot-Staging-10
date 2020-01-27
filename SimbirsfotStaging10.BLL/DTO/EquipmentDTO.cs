using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SimbirsfotStaging10.BLL.DTO
{
    public class EquipmentDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public string ModelName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public float Size { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int EquipmentTypeId { get; set; }
    }
}
