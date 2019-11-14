using SimbirsfotStaging10.DAL.Entities;
using SimbirsfotStaging10.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimbirsfotStaging10.Data.Mocks
{
    public class MockPlatforms : IAllPlatforms
    {
        public IEnumerable<Platform> Platforms 
        { get
            {
                return new List<Platform>
                {
                    //new Platform { Name = "Для новичков"},
                    //new Platform { Name = "Для опытных"},
                    //new Platform { Name = "Для экстрималов"},
                };
            }
        }

        public Platform getObjectPlatform(int PlatformId)
        {
            throw new NotImplementedException();
        }
    }
}
