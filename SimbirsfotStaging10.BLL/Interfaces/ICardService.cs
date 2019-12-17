using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Infrastructure;

namespace SimbirsfotStaging10.BLL.Interfaces
{
    public interface ICardService
    {
        Task<OperationDetail> AddNew(CardDTO DTO, int userId);
        Task<OperationDetail> Delete(int cardId);
        Task<OperationDetail> Edit(int cardId, CardDTO DTO);
        Task<(CardDTO, OperationDetail)> GetById(int cardId);
        Task<(List<CardDTO>, OperationDetail)> GetAllFromDB();
        Task<(List<CardDTO>, OperationDetail)> GetAllFromDB(int userID);
    }
}
