using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class CreateUpdateCategoryDto
    {
        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category can't be longer than 50 characters")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(50, ErrorMessage = "Description can't be longer than 50 characters")]
        public string Description { get; set; }
    }
}
