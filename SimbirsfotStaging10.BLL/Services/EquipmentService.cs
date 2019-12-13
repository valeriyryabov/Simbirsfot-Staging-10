using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Infrastructure;
using SimbirsfotStaging10.BLL.Interfaces;

namespace SimbirsfotStaging10.BLL.Services
{
    class EquipmentService : IEquipmentService
    {
        public async Task<OperationDetail> AddNew(EquipmentDTO DTO, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationDetail> Delete(int equipmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationDetail> Edit(int equipmentId, EquipmentDTO DTO)
        {
            throw new NotImplementedException();
        }

        public async Task<(EquipmentDTO, OperationDetail)> GetById(int equipmentId)
        {
            throw new NotImplementedException();
        }
    }
}
