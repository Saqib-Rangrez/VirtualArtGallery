using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VirtualArtGallery.entity
{
    public class Artwork
    {
        public int ArtworkID { get; set; }  
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime  CreationDate { get; set; }
        public string Medium {  get; set; }
        public string ImageURL { get; set; }
        public int ArtistID {  get; set; }
        
        public Artwork () { }

        public Artwork (string title, string description, DateTime creationDate, string medium, string imageURL, int artistID,[Optional] int artworkID)
        {
            this.ArtworkID = artworkID;
            this.Title = title;
            this.Description = description;
            this.CreationDate = creationDate;
            this.Medium = medium;
            this.ImageURL = imageURL;
            this.ArtistID = artistID;
        }

        public override string ToString()
        {
            return $"Artwork ID \t:\t{ArtworkID}\nTitle \t\t:\t{Title}\nDescription \t:\t{Description}\nCreation Date \t:\t{CreationDate}\nMedium \t\t:\t{Medium}\nImage URL \t:\t{ImageURL}\nArtist ID \t:\t{ArtistID}";
        }
    }
}
