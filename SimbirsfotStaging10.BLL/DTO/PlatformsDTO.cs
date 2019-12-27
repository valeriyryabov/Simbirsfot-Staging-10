using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SimbirsfotStaging10.DAL.Entities;

namespace SimbirsfotStaging10.BLL.DTO
{
    public class PlatformsDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? DateEnd { get; set; }

        public bool IsReserved { get; set; }
    }
}
