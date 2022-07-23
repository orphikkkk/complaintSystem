using Microsoft.AspNetCore.Mvc.Rendering;

namespace SajhaSabal.Models;

public class UserDetailModel
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? District { get; set; }
    public int State { get; set; }
    public string UserId { get; set; }
}