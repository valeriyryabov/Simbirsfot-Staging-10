using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Infrastructure;

namespace SimbirsfotStaging10.BLL.Interfaces
{
    interface IEquipmentService
    {
        Task<OperationDetail> AddNew(EquipmentDTO DTO, int userId);
        Task<OperationDetail> Delete(int equipmentId);
        Task<OperationDetail> Edit(int equipmentId, EquipmentDTO DTO);
        Task<(EquipmentDTO, OperationDetail)> GetById(int equipmentId);
    }
}
