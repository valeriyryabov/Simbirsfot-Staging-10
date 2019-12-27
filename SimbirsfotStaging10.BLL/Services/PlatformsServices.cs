using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimbirsfotStaging10.DAL.Entities;
using SimbirsfotStaging10.BLL.Interfaces;
using SimbirsfotStaging10.BLL.Infrastructure;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SimbirsfotStaging10.BLL.Services
{
    public class PlatformsServices : IPlatformsService
    {
        private SkiDBContext _context;
        private readonly IUserService _userService;


        public PlatformsServices(SkiDBContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<OperationDetail> AddNewPlatform(PlatformsDTO platformsDTO)
        {
            try
            {
                await _context.Platforms.AddAsync(CreatePlatformEntityFromDTO(platformsDTO));
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new OperationDetail { Succeeded = false, Message = ex.Message };
            }
        }

        async public Task<OperationDetail> DeletePlatform(int platfprmId)
        {
            try
            {
                Platform platform = await _context.Platforms.FindAsync(platfprmId);
                _context.Platforms.Remove(platform);
                await _context.SaveChangesAsync().ConfigureAwait(true);
                return new OperationDetail { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new OperationDetail { Succeeded = true, Message = ex.Message };
            }
        }

        public async Task<OperationDetail> EditPlatform(int platfprmId, PlatformsDTO platformsDTO)
        {
            try
            {
                Platform platform = await _context.Platforms.FindAsync(platfprmId);
                platform.Name = platformsDTO.Name;
                _context.Platforms.Update(platform);
                await _context.SaveChangesAsync().ConfigureAwait(true);
                return new OperationDetail { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new OperationDetail { Succeeded = true, Message = ex.Message };
            }
        }

        public async Task<(PlatformsDTO, OperationDetail)> GetPlatformById(int platfprmId)
        {
            try
            {
                Platform platform = await _context.Platforms.FindAsync(platfprmId);
                return (
                    new PlatformsDTO
                    {
                        Id = platform.Id,
                        Name = platform.Name
                    },
                    new OperationDetail
                    {
                        Succeeded = true
                    }
                );
            }
            catch (Exception ex)
            {
                return (null, new OperationDetail { Succeeded = true, Message = ex.Message });
            }
        }


        public async Task<(List<PlatformsDTO>, OperationDetail)> GetAllPlatformsFromDB()
        {
            try
            {
                List<Platform> PlatformList = new List<Platform>();
                List<PlatformsDTO> Platforms = new List<PlatformsDTO>();
                PlatformList = await _context.Platforms.AsNoTracking().ToListAsync();
                foreach (Platform item in PlatformList)
                {
                    Platforms.Add(
                        new PlatformsDTO
                        {
                            Id = item.Id,
                            Name = item.Name
                        }
                    );
                }
                return (Platforms, new OperationDetail { Succeeded = true });
            }
            catch (Exception ex)
            {
                return (null, new OperationDetail { Succeeded = false, Message = ex.Message });
            }
        }

        static Platform CreatePlatformEntityFromDTO(PlatformsDTO platformDTO)
        {
            var platformEntity = new Platform
            {
                Id = platformDTO.Id,
                Name = platformDTO.Name
            };
            return platformEntity;
        }


        public async Task<(IEnumerable<PlatformsDTO>, OperationDetail)> GetPlatformsWithStateAsync()
        {
            var platforms = _context.Platforms.ToArray();

            var curUser = await _userService.GetCurrentUserAsync();
            if (curUser == null)
                return await GetAllPlatformsFromDB();

            var userCards = await _context.Cards
                .Where(c => c.UserId == curUser.Id)
                .Include(c => c.CardPlatformItemList)
                .ToArrayAsync();                
              
            var userPlatLatestCardTerms = userCards
                .SelectMany(c => c.CardPlatformItemList)
                .GroupBy(cp => cp.Platform)
                .Select(
                    g => CreatePlatformDto(g.Key, g.Max(cp => cp.Card.DateEnd))
                )
                .ToList();

            Array.ForEach(platforms, p =>
            {
                if (!userPlatLatestCardTerms.Any(userPlat => userPlat.Id == p.Id))
                    userPlatLatestCardTerms.Add(CreatePlatformDto(p, null));

            });
            return (userPlatLatestCardTerms, new OperationDetail { Succeeded = true });
        }


        private PlatformsDTO CreatePlatformDto(Platform plat, DateTime? DateEnd)
        {
            return new PlatformsDTO
            {
                Id = plat.Id,
                DateEnd = DateEnd,
                Name = plat.Name
            };
        }


        private bool IsCardActive(Card card) => card.DateEnd <= DateTime.Now;
    }
}
