using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiFirst.Models
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        [ForeignKey("Category")]

        public int CategoryId { get; set; }
        public Category category { get; set; }
    }
}
