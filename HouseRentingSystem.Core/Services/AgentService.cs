using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Services
{
    using Contracts;
    using Infrastructure.Data;
    using Infrastructure.Data.Common;
    using Microsoft.EntityFrameworkCore;

    public class AgentService : IAgentService
    {
        private readonly IRepository repo;

        public AgentService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<bool> ExistById(string userId)
        {
            return await repo.All<Agent>()
                .AnyAsync(a => a.UserId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExists(string phoneNumber)
        {
            return await repo.All<Agent>()
                .AnyAsync(a => a.PhoneNumber == phoneNumber);
        }

        public async Task<bool> UserHasRent(string userId)
        {
            return await repo.All<House>()
                .AnyAsync(h => h.RenterId == userId);
        }

        public async Task Create(string userId, string phoneNumber)
        {
            var agent = new Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            };

            await repo.AddAsync(agent);
            await repo.SaveChangesAsync();
        }
    }
}
