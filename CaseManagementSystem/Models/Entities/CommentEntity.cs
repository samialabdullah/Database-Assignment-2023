using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CaseManagementSystem.Models.Entities;

internal class CommentEntity
{
    [Key]
    public int Id { get; set; }
    public string? Text { get; set; }

    [Required]
    public DateTime TimingAt { get; set; } = DateTime.Now;

    [Required]
    [ForeignKey(nameof(Situation))]

    public int SituationId { get; set; }

    [Required]
    [ForeignKey(nameof(Employee))]
    public int CustomerServiceEmployeeId { get; set; }

    public SituationEntity Situation { get; set; } = null!;

    public CustomerServiceEmployeeEntity Employee { get; set; } = null!;
}
