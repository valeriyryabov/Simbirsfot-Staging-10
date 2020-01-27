using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimbirsfotStaging10.DAL.Data;
using SimbirsfotStaging10.DAL.Entities;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Infrastructure;
using SimbirsfotStaging10.BLL.Interfaces;

namespace SimbirsfotStaging10.BLL.Services
{
    public class EquipmentTypeService : IEquipmentTypeService
    {
        private SkiDBContext _context;

        public EquipmentTypeService(SkiDBContext context)
        {
            _context = context;
        }

        public async Task<OperationDetail> AddNewAsync(EquipmentTypeDTO DTO)
        {
            try
            {
                await _context.EquipmentTypes.AddAsync(CreateEntityFromDTO(DTO));
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true };
            }
            catch(Exception ex)
            {
                return new OperationDetail { Succeeded = false, Message = ex.Message };
            }
        }

        public Task<OperationDetail> DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationDetail> EditAsync(int Id, EquipmentTypeDTO DTO)
        {
            throw new NotImplementedException();
        }

        public async Task<(List<EquipmentTypeDTO>, OperationDetail)> GetAllFromDBAsync()
        {
            try
            {
                List<EquipmentTypeDTO> dtoList = new List<EquipmentTypeDTO>();
                var entityList = await _context.EquipmentTypes.AsNoTracking().ToListAsync();
                foreach(EquipmentType item in entityList)
                {
                    dtoList.Add(
                            new EquipmentTypeDTO
                            {
                                Id = item.Id,
                                Name = item.Name,
                                SizeAge = item.AgeSize
                            }
                        );
                }
                return (dtoList, new OperationDetail { Succeeded = true });
            }
            catch(Exception ex)
            {
                return (null, new OperationDetail { Succeeded = false, Message = ex.Message });
            }
        }

        public async Task<(EquipmentTypeDTO, OperationDetail)> GetByIdAsync(int Id)
        {
            try
            {
                return (null, null);
            }
            catch (Exception ex)
            {
                return (null, new OperationDetail { Succeeded = false, Message = ex.Message });
            }

        }


        private EquipmentType CreateEntityFromDTO(EquipmentTypeDTO dto)
        {
            return new EquipmentType
            {
                Id = dto.Id,
                Name = dto.Name,
                AgeSize = dto.SizeAge
            };
        }
    }
}
