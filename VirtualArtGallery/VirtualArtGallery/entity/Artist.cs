using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.entity
{
    public class Artist
    {
        public int ArtistID { get; set; }  
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public string Website { get; set; }
        public string ContactInformation { get; set; }

        public Artist() { }

        public Artist(int artistID, string name, string biography, DateTime birthDate, string nationality, string website, string contactInformation)
        {
            this.ArtistID = artistID;
            this.Name = name;
            this.Biography = biography;
            this.BirthDate = birthDate;
            this.Nationality = nationality;
            this.Website = website;
            this.ContactInformation = contactInformation;
        }

        public override string ToString()
        {
            return $"Artist ID \t:\t{ArtistID}\nName \t\t:\t{Name}\nBiography \t:\t{Biography}\nBirth Date \t:\t{BirthDate}\nNationality \t:\t{Nationality}\nWebsite \t:\t{Website}\nContact \t:\t{ContactInformation}";
        }
    }
}

