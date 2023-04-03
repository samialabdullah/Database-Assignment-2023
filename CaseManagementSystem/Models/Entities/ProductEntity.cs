using System.ComponentModel.DataAnnotations;

namespace CaseManagementSystem.Models.Entities
{
    internal class ProductEntity 
    {
        [Key]
        public string ArticleNumber { get; set; }  = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public decimal Price { get; set; }
    }
}
