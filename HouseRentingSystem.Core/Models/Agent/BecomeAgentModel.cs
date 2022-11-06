namespace HouseRentingSystem.Core.Models.Agent
{
    using System.ComponentModel.DataAnnotations;
    using static HouseRentingSystem.Infrastructure.Data.DataConstants.Agent;


    public class BecomeAgentModel
    {
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
