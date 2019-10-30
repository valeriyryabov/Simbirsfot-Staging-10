﻿using System;
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
        // v_1
        //Task AddNewCard(CardDTO cardDTO);
        //void DeleteCard(int cardId);
        // void EditCard(int cardId, CardDTO cardDTO);

        // v_2
        Task<OperationDetail> AddNewCard(CardDTO cardDTO);
        Task<OperationDetail> DeleteCard(int cardId);
        Task<OperationDetail> EditCard(int cardId, CardDTO cardDTO);
        Task<(CardDTO, OperationDetail)> GetCardById(int cardId);
    }
}
