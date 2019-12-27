using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimbirsfotStaging10.BLL.Infrastructure;
using SimbirsfotStaging10.DAL.Entities;

namespace SimbirsfotStaging10.BLL.Interfaces
{
    public interface ICardPlatformService
    {
        Task<OperationDetail> AddCardPlatform(int cardId, int platformId);
        Task<OperationDetail> AddCardWithPlatforms(int cardId, IEnumerable<int> platformIds);
    }
}
