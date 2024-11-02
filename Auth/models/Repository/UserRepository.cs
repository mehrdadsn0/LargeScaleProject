namespace Auth.Models.Repository;

public class UserRepository
{
    private readonly Context _dbContext;
    public UserRepository(Context context)
    {
        _dbContext = context;
    }

    public User? GetUserByEmail(string email)
    {
        return _dbContext.Users.Where(u => u.Email == email).FirstOrDefault();
    }

    public User? GetUserById(int id)
    {
        return _dbContext.Users.Find(id);
    }

    public (bool, string) AddUser(User user)
    {
        try
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return (true, user.Email);
        }
        catch
        {
            return (false, $"could not create bew user: {user.Email}");
        }
    }

    public User? EditUser(User user)
    {
        User? existingUser = _dbContext.Users.Find(user.Id);
        if (user == null)
        {
            return user;
        }

        if (user.Email != existingUser!.Email)
        {
            existingUser.Email = user.Email;
        }

        if (user.PasswordHash != existingUser.PasswordHash)
        {
            existingUser.PasswordHash = user.PasswordHash;
        }
        _dbContext.SaveChanges();
        return existingUser;
    }

}