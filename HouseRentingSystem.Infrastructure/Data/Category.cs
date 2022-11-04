namespace HouseRentingSystem.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Category;

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public List<House> Houses { get; set; } = new List<House>();
    }
}
