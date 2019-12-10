﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SimbirsfotStaging10.DAL.Entities
{
    public class Platform
    {
        private DbContextOptions options;

        //public Platform(DbContextOptions options)
        //{
        //    this.options = options;
        //}

        public int Id { get; set; }

        [Column(TypeName="varchar(255)")]
        public string Name { get; set; }

        
        public List<CardPlatformItem> CardPlatformItemList { get; set; }

        public List<EventLog> EventLogList { get; set; }     
    }
}
