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
        public int UserId { get; set; }     //Primary Key
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }
        public List<FavoriteArtwork> FavoriteArtworks { get; set; }    // List of references to ArtworkIDs


        public Users() { }
        public Users(int userId, string userName, string password, string email, string firstName, string lastName, DateTime dateOfBirth, string profilePicture,[Optional] List<FavoriteArtwork> favoriteArtworks)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.ProfilePicture = profilePicture;
            this.FavoriteArtworks = favoriteArtworks;
        }

        public override string ToString()
        {
            return $"userId \t\t:\t{UserId}\nUserName \t:\t{UserName}\nEmail \t\t:\t{Email}\nFirstName \t:\t{FirstName}\nLastName \t:\t{LastName}\nDateOfBirth \t:\t{DateOfBirth}\nProfilePicture \t:\t{ProfilePicture}";
        }
    }
}
