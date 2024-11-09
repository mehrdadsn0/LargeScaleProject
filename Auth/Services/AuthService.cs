using Auth.Dtos;
using Auth.Models;
using Auth.Models.Repository;
using Auth.Utilities;

namespace Auth.Services;

public class AuthService
{
    private readonly UserRepository _repo;
    public AuthService(UserRepository repo)
    {
        _repo = repo;
    }

    public User? GetUserById(int id)
    {
        return _repo.GetUserById(id);
    }
    public User? GetUserByEmail(string email)
    {
        return _repo.GetUserByEmail(email);
    }
    public (bool, string) SignUp(SignUpRequestDto input)
    {
        User newUser = new User(email: input.Email, passwordHash: PasswordHashManager.HashPasword(input.Password), phoneNumber: input.PhoneNumber);
        return _repo.AddUser(newUser);
    }

    public (bool, string) SignIn(SignInRequestDto input)
    {
        User? user = _repo.GetUserByEmail(input.Email);
        if (user == null)
        {
            return (false, "user not found");
        }
        if (PasswordHashManager.VerifyPasswordHash(input.Password, user.PasswordHash))
        {
            return (true, "successful");
        }
        return (false, "password does not match");
    }

    public (bool, string) ChangePassword(SignUpRequestDto input)
    {
        User? user = _repo.GetUserByEmail(input.Email);
        if (user is null)
        {
            return (false, "user not found");
        }
        user.PasswordHash = PasswordHashManager.HashPasword(input.Password);
        var res = _repo.EditUser(user);
        return (true, "password changed successfully");

    }
}