using System.ComponentModel.DataAnnotations;

namespace WebApiFirst.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public ICollection<ProductModel> Products { get; set; }


    }
}
