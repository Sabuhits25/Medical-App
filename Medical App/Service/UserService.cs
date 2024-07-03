using Medical_App.Exceptions;
using Medical_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_App.Service
{
    public class UserService
    {
        private User[] users = new User[0];

        public void AddUser(User user)
        {
            ValidateUser(user);

           
            foreach (var existingUser in users)
            {
                if (existingUser != null && string.Equals(existingUser.Email, user.Email, StringComparison.OrdinalIgnoreCase))
                {
                    throw new AlreadyExistException("Bu Emailde istfiadeci movcuddur!.");
                }
            }

            
            var newUsers = new User[users.Length + 1];
            Array.Copy(users, newUsers, users.Length);
            newUsers[newUsers.Length - 1] = user;
            users = newUsers;
        }

        public User Login(string email, string password)
        {
            
            foreach (var user in users)
            {
                if (user != null && string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase) && user.Password == password)
                {
                    return user;
                }
            }


            throw new NotFoundException("User not found with the provided email and password.");
        }

        private void ValidateUser(User user)
        {
           
            if (string.IsNullOrWhiteSpace(user.Fullname))
            {
                throw new ArgumentException("Fullname cannot be empty.");
            }

            if (!IsValidEmail(user.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 8 || !HasUpperCase(user.Password) || !HasLowerCase(user.Password))
            {
                throw new ArgumentException("Password 8 simvoldan az olmamalidir!Boyuk ve kicik herflerden ibaret olmalidi.");
            }
        }

        private bool IsValidEmail(string email)
        {
            
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool HasUpperCase(string input)
        {
            
            foreach (char c in input)
            {
                if (char.IsUpper(c)) return true;
            }
            return false;
        }

        private bool HasLowerCase(string input)
        {
          
            foreach (char c in input)
            {
                if (char.IsLower(c)) return true;
            }
            return false;
        }
    }
}
