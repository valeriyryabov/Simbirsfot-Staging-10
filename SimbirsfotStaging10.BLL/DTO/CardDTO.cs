using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SimbirsfotStaging10.DAL.Entities;

namespace SimbirsfotStaging10.BLL.DTO
{
    public class CardDTO
    {
        // v_1
        /*
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsValid { get; set; }
        public User Owner { get; set; }
        */

        // v_2
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime DateBegin { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }
    }
}
