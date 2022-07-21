namespace SajhaSabal.Models;

public class ComplaintModel
{
    public int Id { get; set; }
    public string? Title { get; set; }  
    public string? Description { get; set; }
    public string? Status { get; set; }
    public int DepartmentId { get; set; }
    public string UserId { get; set; }
}

public class ComplaintViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }  
    public string? Description { get; set; }
    public string? Status { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
}