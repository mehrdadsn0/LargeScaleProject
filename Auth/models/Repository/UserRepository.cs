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

    public User AddUser(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }

    public User? EditUser(int id, User user)
    {
        User? existingUser = _dbContext.Users.Find(id);
        if(user == null){
            return user;
        }

        if(user.Email != existingUser!.Email){
         existingUser.Email = user.Email;   
        }

        if(user.PasswordHash != existingUser.PasswordHash)
        {
            existingUser.PasswordHash = user.PasswordHash;
        }
        _dbContext.SaveChanges();
        return existingUser;
    }

}