using System;
using System.Collections.Generic;
using System.Text;
using SimbirsfotStaging10.DAL.Data;
using SimbirsfotStaging10.DAL.Entities;
using SimbirsfotStaging10.DAL.Data;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Interfaces;
using System.Threading.Tasks;
using SimbirsfotStaging10.BLL.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace SimbirsfotStaging10.BLL.Services
{
    public class CardService : ICardService
    {
        private SkiDBContext _context;
        private List<CardDTO> Cards { get; set; }

        public CardService(SkiDBContext context)
        {
            _context = context;
        }

        public async Task<OperationDetail> AddNewCard(CardDTO cardDTO)
        {
            try
            {
                await _context.Cards.AddAsync(CreateCardEntityFromDTO(cardDTO));
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true }; 
            }
            catch (Exception ex)
            {
                return new OperationDetail { Succeeded = false, Message = ex.Message };
            }
        }

        public async Task<OperationDetail> DeleteCard(int cardId)
        {
            try
            {
                Card card = await _context.Cards.FindAsync(cardId);
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true };
            }
            catch(Exception ex)
            {
                return new OperationDetail { Succeeded = true, Message = ex.Message };
            }
        }

        public async Task<OperationDetail> EditCard(int cardId, CardDTO cardDTO)
        {
            try
            {
                Card card = await _context.Cards.FindAsync(cardId);
                card.DateBegin = cardDTO.DateBegin;
                card.DateEnd = cardDTO.DateEnd;
                _context.Cards.Update(card);
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new OperationDetail { Succeeded = true, Message = ex.Message };
            }
        }

        public async Task<(CardDTO, OperationDetail)> GetCardById(int cardId)
        {
            try
            {
                Card card = await _context.Cards.FindAsync(cardId);
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
                return (null, new OperationDetail { Succeeded = true, Message = ex.Message });
            }
        }

        public async Task<(List<CardDTO>, OperationDetail)> GetAllCardsFromDB()
        {
            try
            {
                List<Card> CardsList = new List<Card>();
                Cards = new List<CardDTO>(); 
                CardsList = await _context.Cards.AsNoTracking().ToListAsync();
                foreach(Card item in CardsList)
                {
                    Cards.Add(
                        new CardDTO
                        {
                            Id = item.Id,
                            DateBegin = item.DateBegin,
                            DateEnd = item.DateEnd,
                        }
                    );
                }
                return (Cards, new OperationDetail { Succeeded = true });
            }
            catch (Exception ex)
            {
                return (null, new OperationDetail { Succeeded = true, Message = ex.Message });
            }
        }


        static Card CreateCardEntityFromDTO(CardDTO cardDTO)
        {
            var cardEntity = new Card
            {
                Id = cardDTO.Id,
                DateBegin = cardDTO.DateBegin,
                DateEnd = cardDTO.DateEnd,
            };
            return cardEntity;
        }
    }
}
