using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SimbirsfotStaging10.DAL.Entities;

namespace SimbirsfotStaging10.BLL.DTO
{
    public class CardDTO
    {
        /*
        [Required]
        public int UserId { get; set; }
        */

        [Required]
        public DateTime DateBegin { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }
    }
}
