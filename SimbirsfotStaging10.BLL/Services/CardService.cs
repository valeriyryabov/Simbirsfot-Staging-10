using System;
using System.Collections.Generic;
using System.Text;
using SimbirsfotStaging10.DAL.Data;
using SimbirsfotStaging10.DAL.Entities;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Interfaces;
using System.Threading.Tasks;

namespace SimbirsfotStaging10.BLL.Services
{
    public class CardService : ICardService
    {
        public SkiDBContext.EFDBContextFactory contextFactory = new SkiDBContext.EFDBContextFactory();
        private SkiDBContext _context;

        public CardService(SkiDBContext context)
        {
            _context = context;
        }

        public async Task AddNewCard(CardDTO cardDTO)
        {
            await _context.AddAsync<Card>(CreateCardEntityFromDTO(cardDTO)).ConfigureAwait(true);
        }

        public async void DeleteCard(int cardId)
        {
            Card card = _context.Find<Card>(cardId); // получили объект из базы
            _context.Remove<Card>(card);
            await _context.SaveChangesAsync().ConfigureAwait(true);
        }

        public async void EditCard(int cardId, CardDTO cardDTO)
        {
            Card card = _context.Find<Card>(cardId); // получили объект из базы
            card.DateBegin = cardDTO.DateBegin;
            card.DateEnd = cardDTO.DateEnd;
            _context.Update<Card>(card);
            await _context.SaveChangesAsync().ConfigureAwait(true);
        }

        static Card CreateCardEntityFromDTO(CardDTO cardDTO)
        {
            var cardEntity = new Card
            {
                UserId = cardDTO.UserId,
                Owner = cardDTO.Owner,
                DateBegin = cardDTO.DateBegin,
                DateEnd = cardDTO.DateEnd,
            };
            return cardEntity;
        }

        private Card GetCardById(int cardId)
        {
            return _context.Cards.Find(cardId);
        }
    }
}
