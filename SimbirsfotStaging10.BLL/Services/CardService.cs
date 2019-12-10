using System;
using System.Collections.Generic;
using System.Text;
using SimbirsfotStaging10.DAL.Data;
using SimbirsfotStaging10.DAL.Entities;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Interfaces;
using System.Threading.Tasks;
using SimbirsfotStaging10.BLL.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace SimbirsfotStaging10.BLL.Services
{
    public class CardService : ICardService
    {
        private SkiDBContext _context;

        public CardService(SkiDBContext context)
        {
            _context = context;
        }

        public async Task<OperationDetail> AddNew(CardDTO dTO, int userId)
        {
            try
            {
                await _context.Cards.AddAsync(CreateEntityFromDTO(dTO, userId));
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true }; 
            }
            catch (Exception ex)
            {
                return new OperationDetail { Succeeded = false, Message = ex.Message + "\n" + ex.InnerException };
            }
        }

        public async Task<OperationDetail> AddNew(CardDTO dTO, int userId, User owner)
        {
            try
            {
                await _context.Cards.AddAsync(CreateEntityFromDTO(dTO, userId, owner));
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new OperationDetail { Succeeded = false, Message = ex.Message + "\n" + ex.InnerException };
            }
        }

        public async Task<OperationDetail> Delete(int id)
        {
            try
            {
                Card card = await _context.Cards.FindAsync(id);
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true };
            }
            catch(Exception ex)
            {
                return new OperationDetail { Succeeded = false, Message = ex.Message };
            }
        }

        public async Task<OperationDetail> Edit(int id, CardDTO dTO)
        {
            try
            {
                Card card = await _context.Cards.FindAsync(id);
                card.DateBegin = dTO.DateBegin;
                card.DateEnd = dTO.DateEnd;
                _context.Cards.Update(card);
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new OperationDetail { Succeeded = false, Message = ex.Message };
            }
        }

        public async Task<(CardDTO, OperationDetail)> GetById(int id)
        {
            try
            {
                Card card = await _context.Cards.FindAsync(id);
                return (
                    new CardDTO
                    {
                        Id = card.Id,
                        DateBegin = card.DateBegin,
                        DateEnd = card.DateEnd,
                    },
                    new OperationDetail
                    {
                        Succeeded = true
                    }
                );
            }
            catch (Exception ex)
            {
                return (null, new OperationDetail { Succeeded = false, Message = ex.Message });
            }
        }

        public async Task<(List<CardDTO>, OperationDetail)> GetAllFromDB()
        {
            try
            {
                List<CardDTO> dTOList = new List<CardDTO>(); 
                var entityList = await _context.Cards.AsNoTracking().ToListAsync();
                foreach(Card item in entityList)
                {
                    dTOList.Add(
                        new CardDTO
                        {
                            Id = item.Id,
                            DateBegin = item.DateBegin,
                            DateEnd = item.DateEnd,
                            UserId = item.UserId
                        }
                    );
                }
                return (dTOList, new OperationDetail { Succeeded = true });
            }
            catch (Exception ex)
            {
                return (null, new OperationDetail { Succeeded = false, Message = ex.Message });
            }
        }

        public async Task<(List<CardDTO>, OperationDetail)> GetAllFromDB(int userID)
        {
            try
            {
                List<CardDTO> dTOList = new List<CardDTO>();
                var entityList = await _context.Cards.AsNoTracking().ToListAsync();
                foreach (Card item in entityList)
                {
                    if (item.UserId == userID)
                    {
                        dTOList.Add(
                            new CardDTO
                            {
                                Id = item.Id,
                                DateBegin = item.DateBegin,
                                DateEnd = item.DateEnd,
                            }
                        );
                    }
                }
                return (dTOList, new OperationDetail { Succeeded = true });
            }
            catch (Exception ex)
            {
                return (null, new OperationDetail { Succeeded = false, Message = ex.Message });
            }
        }

        private Card CreateEntityFromDTO(CardDTO dTO, int userId)
        {
            return new Card
            {
                Id = dTO.Id,
                UserId = userId,
                DateBegin = dTO.DateBegin,
                DateEnd = dTO.DateEnd,
            };
        }

        private Card CreateEntityFromDTO(CardDTO dTO, int userId, User owner)
        {
            return new Card
            {
                Id = dTO.Id,
                UserId = userId,
                DateBegin = dTO.DateBegin,
                DateEnd = dTO.DateEnd,
                Owner = owner
            };
        }
    }
}
