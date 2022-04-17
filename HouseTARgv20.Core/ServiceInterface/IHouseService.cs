using System;
using System.Threading.Tasks;
using HouseTARgv20.Core.Domain;
using HouseTARgv20.Core.Dto;

namespace HouseTARgv20.Core.ServiceInterface
{
    public interface IHouseService : IApplicationService
    {
        Task<HouseDomain> Add(HouseDto dto);
        Task<HouseDomain> Edit(Guid id);
        Task<HouseDomain> Update(HouseDto dto);
        Task<HouseDomain> Delete(Guid id);
    }
}
