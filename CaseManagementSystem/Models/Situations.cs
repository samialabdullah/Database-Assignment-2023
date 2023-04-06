namespace CaseManagementSystem.Models;

internal class Situations
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public string Condition { get; set; } = null!;
    public Customers Customer { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}
