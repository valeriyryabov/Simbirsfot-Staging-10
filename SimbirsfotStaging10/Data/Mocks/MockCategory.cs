using SimbirsfotStaging10.DAL.Entities;
using SimbirsfotStaging10.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimbirsfotStaging10.Data.Mocks
{
    public class MockCategory : IPlatformsCategory
    {
        public IEnumerable<CardPlatformItem> AllCategories
        {
            get
            {
                return new List<CardPlatformItem>
                {
                    new CardPlatformItem{ CardId = 1,PlatformId = 1 },
                    new CardPlatformItem{ CardId = 2,PlatformId = 2 }
                };
            }
        }

    }
}
