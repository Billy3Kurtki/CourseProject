namespace Microservice.Models
{
    public abstract class UserBase
    {
        int Id { get; }
        string fullName { get; }
        string Login { get; }
        string Password { get; }
        string Email { get; }
        DateTime Birthdate { get; }

        
    }
}
