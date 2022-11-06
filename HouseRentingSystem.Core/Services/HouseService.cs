namespace HouseRentingSystem.Core.Services
{
    using Contracts;
    using Infrastructure.Data;
    using Infrastructure.Data.Common;
    using Microsoft.EntityFrameworkCore;
    using Models.House;

    public class HouseService : IHouseService
    {
        private readonly IRepository repo;

        public HouseService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<HouseHomeModel>> LastThreeHouses()
        {
            return await repo.AllReadonly<House>()
                .OrderByDescending(h => h.Id)
                .Select(h => new HouseHomeModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    ImageUrl = h.ImageUrl
                })
                .Take(3)
                .ToListAsync();
        }
    }
}
