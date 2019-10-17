﻿using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Entities
{
    [Table("EventLog")]
    public class EventLog
    {
        public int Id { get; set; }

        public int EventType { get; set; }

        public int CardId { get; set; }

        public int PlatformId { get; set; }

        [Column(TypeName ="date")]
        public DateTime Date { get; set; }

        [ForeignKey("CardId")]
        public Card Card { get; set; }

        public Platform Platform { get; set; }
    }   
}
