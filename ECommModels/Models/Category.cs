using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eCommerceUdemy.Models
{
    public class Category
    {
        //[Key]
        public int Id { get; set; }
        [DisplayName("Category name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [DisplayName("Display order")]
        [Range(1, 100, ErrorMessage="Display order must be between 1-100")]
        public int DisplayOrder { get; set; } 
        
    }
}
