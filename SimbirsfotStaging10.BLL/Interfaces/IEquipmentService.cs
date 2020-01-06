using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Infrastructure;

namespace SimbirsfotStaging10.BLL.Interfaces
{
    public interface IEquipmentService
    {
        Task<OperationDetail> AddNewAsync(EquipmentDTO DTO);
        Task<OperationDetail> DeleteAsync(int Id);
        Task<OperationDetail> EditAsync(int Id, EquipmentDTO DTO);
        Task<(EquipmentDTO, OperationDetail)> GetByIdAsync(int Id);
        Task<(List<EquipmentDTO>, OperationDetail)> GetAllFromDBAsync();
    }
}
