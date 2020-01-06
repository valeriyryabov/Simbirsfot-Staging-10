using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Infrastructure;
using SimbirsfotStaging10.BLL.Interfaces;
using SimbirsfotStaging10.DAL.Data;
using SimbirsfotStaging10.DAL.Entities;

namespace SimbirsfotStaging10.BLL.Services
{
    public class EquipmentService : IEquipmentService
    {
        private SkiDBContext _context;

        public EquipmentService(SkiDBContext context)
        {
            _context = context;
        }

        public async Task<OperationDetail> AddNewAsync(EquipmentDTO DTO)
        {
            try
            {
                await _context.EquipmentSet.AddAsync(CreateEntityFromDTO(DTO));
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true };
            }
            catch(Exception ex)
            {
                return new OperationDetail { Succeeded = false, Message = ex.Message };
            }
        }

        public async Task<OperationDetail> DeleteAsync(int Id)
        {
            try
            {
                Equipment equipment = await _context.EquipmentSet.FindAsync(Id);
                _context.EquipmentSet.Remove(equipment);
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new OperationDetail { Succeeded = false, Message = ex.Message };
            }
        }

        public async Task<OperationDetail> EditAsync(int Id, EquipmentDTO DTO)
        {
            try
            {
                Equipment equipment = await _context.EquipmentSet.FindAsync(Id);
                equipment.Name = DTO.Name;
                equipment.Type = DTO.Type;
                equipment.IsDeleted = DTO.IsDeleted;
                _context.EquipmentSet.Update(equipment);
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new OperationDetail { Succeeded = false, Message = ex.Message };
            }
        }

        public async Task<(List<EquipmentDTO>, OperationDetail)> GetAllFromDBAsync()
        {
            try
            {
                List<EquipmentDTO> dTOList = new List<EquipmentDTO>();
                var entityList = await _context.EquipmentSet.AsNoTracking().ToListAsync();
                foreach (Equipment item in entityList)
                {
                    dTOList.Add(
                        new EquipmentDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Type = item.Type,
                            IsDeleted = item.IsDeleted
                        }
                    );
                }
                return (dTOList, new OperationDetail { Succeeded = true });
            }
            catch (Exception ex)
            {
                return (null, new OperationDetail { Succeeded = false, Message = ex.Message });
            }
        }

        public async Task<(EquipmentDTO, OperationDetail)> GetByIdAsync(int Id)
        {
            try
            {
                Equipment equipment = await _context.EquipmentSet.FindAsync(Id);
                return (
                    new EquipmentDTO
                    {
                        Id = equipment.Id,
                        Name = equipment.Name,
                        Type = equipment.Type,
                        IsDeleted = equipment.IsDeleted
                    },
                    new OperationDetail
                    { Succeeded = true }
                );
            }
            catch (Exception ex)
            {
                return (null, new OperationDetail { Succeeded = false, Message = ex.Message });
            }
        }


        private Equipment CreateEntityFromDTO(EquipmentDTO dTO)
        {
            return new Equipment
            {
                Id = dTO.Id,
                Name = dTO.Name,
                Type = dTO.Type,
                IsDeleted = dTO.IsDeleted
            };
        }
    }
}
