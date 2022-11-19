namespace HouseRentingSystem.Core.Contracts
{
    public interface IAgentService
    {
        Task<bool> ExistById(string userId);

        Task<bool> UserWithPhoneNumberExists(string phoneNumber);

        Task<bool> UserHasRent(string userId);

        Task Create(string userId, string phoneNumber);

        Task<int> GetAgentId(string userId);
    }
}
