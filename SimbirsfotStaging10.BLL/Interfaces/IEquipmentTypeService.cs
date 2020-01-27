using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Infrastructure;

namespace SimbirsfotStaging10.BLL.Interfaces
{
    public interface IEquipmentTypeService
    {
        Task<OperationDetail> AddNewAsync(EquipmentTypeDTO DTO);
        Task<OperationDetail> DeleteAsync(int Id);
        Task<OperationDetail> EditAsync(int Id, EquipmentTypeDTO DTO);
        Task<(EquipmentTypeDTO, OperationDetail)> GetByIdAsync(int Id);
        Task<(List<EquipmentTypeDTO>, OperationDetail)> GetAllFromDBAsync();
    }
}
