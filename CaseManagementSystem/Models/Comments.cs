namespace CaseManagementSystem.Models;

internal class Comments
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int SituationId { get; set; }
}
