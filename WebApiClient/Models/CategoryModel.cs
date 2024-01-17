using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApiClient.Models
{
    public class CategoryModel
    {
       [JsonPropertyName("id")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [Required]
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
       // public ICollection<ProductModel> Products { get; set; }
    }
}
