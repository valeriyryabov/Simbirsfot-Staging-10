using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace SimbirsfotStaging10.DAL.Entities
{
    [Table("EventLog")]
    public class EventLog
    {
        public int Id { get; set; }

        public int EventType { get; set; }

        public int? CardId { get; set; }

        public int? PlatformId { get; set; }

        [Column(TypeName ="date")]
        public DateTime Date { get; set; }

        public string Message { get; set; }

        public Card Card { get; set; }

        public Platform Platform { get; set; }
    }   
}
