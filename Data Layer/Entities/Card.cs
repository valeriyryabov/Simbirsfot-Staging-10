using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Entities
{
    public class Card
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime DateBegin { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime DateEnd { get; set; }

        public User Owner { get; set; }

        public List<CardPlatformItem> CardPlatformItemList { get; set; }
    }
}
