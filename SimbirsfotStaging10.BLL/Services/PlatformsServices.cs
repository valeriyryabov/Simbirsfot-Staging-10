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

namespace SimbirsfotStaging10.BLL.Services
{
    public class PlatformsServices : IPlatformsService
    {
        private SkiDBContext _context;
        private List<PlatformsDTO> Platforms { get; set; }  

        public PlatformsServices(SkiDBContext context)
        {
            _context = context;
        }

        public async Task<OperationDetail> AddNewPlatform(PlatformsDTO platformsDTO)
        {
            try
            {
                await _context.Platforms.AddAsync(CreatePlatformEntityFromDTO(platformsDTO)).ConfigureAwait(true);
                await _context.SaveChangesAsync().ConfigureAwait(true);
                return new OperationDetail { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new OperationDetail { Succeeded = false, Message = ex.Message };
            }

            //throw new NotImplementedException();
        }

        async public Task<OperationDetail> DeletePlatform(int platfprmId)
        {
            try
            {
                Platform platform = await _context.Platforms.FindAsync(platfprmId);
                _context.Platforms.Remove(platform);
                await _context.SaveChangesAsync().ConfigureAwait(true);
                return new OperationDetail { Succeeded = true };
                //Card card = await _context.Cards.FindAsync(cardId);
                //_context.Cards.Remove(card);
                //await _context.SaveChangesAsync().ConfigureAwait(true);
                //return new OperationDetail { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new OperationDetail { Succeeded = true, Message = ex.Message };
            }
            // throw new NotImplementedException();
        }

        //public Task<OperationDetail> EditPlatform(int platfprmId, PlatformsDTO platformsDTO)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<OperationDetail> EditPlatform(int platfprmId,PlatformsDTO platformsDTO)
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
            //throw new NotImplementedException();
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
            // throw new NotImplementedException();
        }

    

        //Task<(List<PlatformsDTO>, OperationDetail)> IPlatformsService.GetAllPlatformsFromDB()
        //{

        //    throw new NotImplementedException();
        //}
       
        static Platform CreatePlatformEntityFromDTO(PlatformsDTO platformDTO)
        {
            var platformEntity = new Platform
            {
                Id =platformDTO.Id,
                Name=platformDTO.Name
            };
            return platformEntity;
        }

    }
}
