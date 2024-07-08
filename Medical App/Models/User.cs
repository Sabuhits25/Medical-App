using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_App.Models
{
    public class User : BaseEntity
    {
        private string _password;
        private string _email;

        public string Fullname { get; set; }

        public string Email
        {
            get => _email;
            set
            {
                if (IsValidEmail(value))
                {
                    _email = value;
                }
                else
                {
                    throw new ArgumentException("Invalid email format.");
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (IsValidPassword(value))
                {
                    _password = value;
                }
                else
                {
                    throw new ArgumentException("(En azi 8 simvol hem boyuk hem de kicik herf olmalidir.");
                }
            }
        }

        public bool IsValidPassword(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }

            bool hasUpperCase = false;
            bool hasLowerCase = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) 
                { 
                 hasUpperCase = true;

                }

                if (char.IsLower(c)) 
                { 
                hasLowerCase = true;

                } 
            }
          
               return hasUpperCase && hasLowerCase;

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
    }

}
