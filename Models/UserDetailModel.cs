namespace SajhaSabal.Models;

public class UserDetailModel
{
    public int Id { get; set; }
    public string? Title { get; set; }  
    public string? Description { get; set; }
    public string? Status { get; set; }
    public int DepartmentId { get; set; }
    public int UserId { get; set; }
}