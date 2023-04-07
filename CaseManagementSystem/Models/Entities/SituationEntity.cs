using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CaseManagementSystem.Models.Entities;

internal class SituationEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    [Required]
    public string Condition { get; set; } = "EjPåbörjad";

    [Required]
    [ForeignKey(nameof(Customer))]
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
    public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
}

public enum SituationCondition
{
    EjPåbörjad,
    Pågående,
    Avslutad
}
