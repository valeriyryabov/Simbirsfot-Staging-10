using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimbirsfotStaging10.DAL.Data;
using SimbirsfotStaging10.DAL.Entities;
using SimbirsfotStaging10.BLL.Infrastructure;
using SimbirsfotStaging10.BLL.DTO;



namespace SimbirsfotStaging10.BLL.Interfaces
{
    public interface IPlatformsService
    {
        Task<OperationDetail> AddNewPlatform(PlatformsDTO platformsDTO);
        Task<OperationDetail> DeletePlatform(int platfprmId);
        Task<OperationDetail> EditPlatform(int platfprmId, PlatformsDTO platformsDTO);
        Task<(PlatformsDTO, OperationDetail)> GetPlatformById(int platfprmId);
       
        Task<(List<PlatformsDTO>, OperationDetail)> GetAllPlatformsFromDB();
        Task<(IEnumerable<PlatformsDTO>, OperationDetail)> GetPlatformsWithStateAsync();
    }
}
