using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimbirsfotStaging10.DAL.Data;
using SimbirsfotStaging10.DAL.Entities;
using SimbirsfotStaging10.BLL.DTO;

namespace SimbirsfotStaging10.BLL.Interfaces
{
    public interface ICardService
    {
        Task AddNewCard(CardDTO cardDTO);
        void DeleteCard(int cardId);
        void EditCard(int cardId, CardDTO cardDTO);
    }
}
