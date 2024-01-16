using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.entity
{
    public class Users
    {
        public int UserId { get; set; }    
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }


        public Users() { }
        public Users(int userId, string userName, string password, string email, string firstName, string lastName, DateTime dateOfBirth, string profilePicture)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.ProfilePicture = profilePicture;
        }

        public override string ToString()
        {
            return $"User ID \t:\t{UserId}\nUsername \t:\t{UserName}\nEmail \t\t:\t{Email}\nFirst Name \t:\t{FirstName}\nLast Name \t:\t{LastName}\nDate Of Birth \t:\t{DateOfBirth}\nProfile Picture\t:\t{ProfilePicture}";
        }
    }
}
