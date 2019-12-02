using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimbirsfotStaging10.DAL.Data;
using SimbirsfotStaging10.DAL.Entities;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Infrastructure;

namespace SimbirsfotStaging10.BLL.Interfaces
{
    public interface ICardService
    {
        Task<OperationDetail> AddNew(CardDTO DTO);
        Task<OperationDetail> Delete(int cardId);
        Task<OperationDetail> Edit(int cardId, CardDTO DTO);
        Task<(CardDTO, OperationDetail)> GetById(int cardId);
        Task<(List<CardDTO>, OperationDetail)> GetAllFromDB();
    }
}
