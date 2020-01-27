using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SimbirsfotStaging10.BLL.DTO
{
    public class EquipmentTypeDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SizeAge { get; set; }

    }
}
