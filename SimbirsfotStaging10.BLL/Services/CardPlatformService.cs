using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SimbirsfotStaging10.BLL.Infrastructure;
using SimbirsfotStaging10.BLL.Interfaces;
using SimbirsfotStaging10.DAL.Data;
using SimbirsfotStaging10.DAL.Entities;
using SimbirsfotStaging10.Logger;

namespace SimbirsfotStaging10.BLL.Services
{
    public class CardPlatformService : ICardPlatformService
    {
        private SkiDBContext _dbContext;
        private ILogger<CardPlatformService> _logger;

        public CardPlatformService(SkiDBContext context, ILogger<CardPlatformService> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<OperationDetail> AddCardPlatform(int cardId, int platformId)
        {
            //async void очень плохо) но там вроде exception'ы внутри ловятся
            return await OperationDetail.TryCatchWithDetailAsync(async delegate
               {
                   await _dbContext.CardPlatformItemSet.AddAsync(
                           new CardPlatformItem
                           {
                               CardId = cardId,
                               PlatformId = platformId
                           });
                   await _dbContext.SaveChangesAsync();
                   _logger.LogInformation(new EventId((int)EventType.CreateCardWithPlatforms),
                       $"Card {cardId} was created with platform {platformId}");
               });
        }

        public async Task<OperationDetail> AddCardWithPlatforms(int cardId, IEnumerable<int> platformIds)
        {
            return await OperationDetail.TryCatchWithDetailAsync(async delegate
            {
                await _dbContext.CardPlatformItemSet.AddRangeAsync(
                    platformIds.Select(platformId => new CardPlatformItem { CardId = cardId, PlatformId = platformId }));
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation(new EventId((int)EventType.CreateCardWithPlatforms),
                       $"Card {cardId} was created with platform {string.Join(',', platformIds)}");
            });
        }
    }
}
