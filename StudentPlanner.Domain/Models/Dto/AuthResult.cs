namespace StudentPlanner.Domain.Models.Dto;

public class AuthResult
{
    public IEnumerable<string> Errors { get; set; }
    public bool Success { get; set; }
    public string Token { get; set; }
}