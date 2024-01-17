
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiFirst.Models
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
