using System;

public class User
{
    [Key, MaxLength(100), MinLength(3)]
    public string Username { get; set; } = null!;
    [MaxLength(100), MinLength(3)]
    public string Password { get; set; } = null!;
}
