using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace SimbirsfotStaging10.DAL.Entities
{
    public class Platform
    {
        public int Id { get; set; }

        [Column(TypeName="varchar(255)")]
        public string Name { get; set; }

        public List<CardPlatformItem> CardPlatformItemList { get; set; }

        public List<EventLog> EventLogList { get; set; }     
    }
}
