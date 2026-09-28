using System.ComponentModel.DataAnnotations;

namespace study_api.Dtos
{
    public record ProductRequestDto
    {
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "At least 4 characteres are required.")]
        public required string Name { get; init; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "At least 3 characteres are required.")]
        public required string Color { get; init; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public required decimal Price { get; init; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public required int Stock {  get; init; }
    }
}
