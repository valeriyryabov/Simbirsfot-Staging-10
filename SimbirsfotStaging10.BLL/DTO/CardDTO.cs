using System;
using System.Collections.Generic;
using System.Text;
using SimbirsfotStaging10.DAL.Entities;

namespace SimbirsfotStaging10.BLL.DTO
{
    public class CardDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsValid { get; set; }
        public User Owner { get; set; }
    }
}
