namespace HouseRentingSystem.Core.Models.House
{
    using System.ComponentModel.DataAnnotations;
    using static HouseRentingSystem.Infrastructure.Data.DataConstants.House;

    public class HouseModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(0.00, PricePerMonthMaxValue,
            ErrorMessage = "Price per month must be a positive number and less than {2} leva.")]
        [Display(Name = "Price Per Month")]
        public decimal PricePerMonth { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<HouseCategoryModel> Categories { get; set; }
            = new List<HouseCategoryModel>();
    }
}
