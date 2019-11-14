using SimbirsfotStaging10.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimbirsfotStaging10.Data.Interface
{
    public interface IPlatformsCategory
    {
        IEnumerable<CardPlatformItem> AllCategories { get; }//получает просто данные
         
    }
}
 