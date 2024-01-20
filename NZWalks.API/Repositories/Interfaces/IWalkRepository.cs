using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DomainModels;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllWalkAsync(string? filterOn=null, string? filterValue=null, string? orderBy=null, 
        bool isAscending=true, int pageNumber = 1, int pageSize=5);
        Task<Walk?> GetWalkAsync(Guid Id);
        Task<Walk> CreateWalkAsync(Walk walk);
        Task<Walk?> UpdateWalkAsync(Guid Id, Walk walk);
        Task<Walk?> DeleteWalkAsync(Guid Id);
    }
}
