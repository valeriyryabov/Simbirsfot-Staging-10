using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SimbirsfotStaging10.DAL.Entities;

namespace SimbirsfotStaging10.BLL.DTO
{
    public class CardDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateBegin { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }

        [Required]
        public int UserId;

        public IEnumerable<int> PlatformIdsWithAccess { get; set; }
    }
}
