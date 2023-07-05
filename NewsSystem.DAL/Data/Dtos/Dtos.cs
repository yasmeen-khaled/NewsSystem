namespace NewsSystem.DAL.Dtos;

public record RegisterDto(string UserName,
string Email,
string Password);

public record LoginDto(string UserName, string Password);

public record TokenDto(string Token, DateTime Expiry);

