namespace HouseRentingSystem.Core.Models.House
{
    using Agent;

    public class HouseDetailsModel : HouseServiceModel
    {
        public string Description { get; init; } = null!;

        public string Category { get; init; } = null!;

        public AgentServiceModel Agent { get; init; }
    }
}
