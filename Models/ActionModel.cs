namespace SajhaSabal.Models;

public class ActionModel
{
    public int Id { get; set; }
    public string? ActionTaken { get; set; }  
    public int ActionBy { get; set; }
    public int ComplainId { get; set; }
}