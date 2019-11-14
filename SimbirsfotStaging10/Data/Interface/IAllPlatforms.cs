using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimbirsfotStaging10.DAL.Entities;

namespace SimbirsfotStaging10.Data.Interface
{
    public interface IAllPlatforms
    {
        IEnumerable<Platform> Platforms { get;  } // Получаем все данные

        Platform getObjectPlatform(int PlatformId);//Получаем конкретный Склон

    }
}
