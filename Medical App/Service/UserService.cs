using Medical_App.Exceptions;
using Medical_App.Models;

namespace Medical_App.Service
{
    public class UserService
    {
        public User Login(string email, string password)
        {
            foreach (User user in DB.Users)
            {
                if (user != null && user.Email == email && user.Password == password)
                {
                    return user;
                }
            }
            throw new NotFoundException("User not found with provided email and password.");
        }

        public void AddUser(User user)
        {
            foreach (User existingUser in DB.Users)
            {
                if (existingUser.Email == user.Email)
                {
                    throw new ArgumentException("A user with this email already exists.");
                }
            }

            Array.Resize(ref DB.Users, DB.Users.Length + 1);
            DB.Users[^1] = user;

        }
    }
}


