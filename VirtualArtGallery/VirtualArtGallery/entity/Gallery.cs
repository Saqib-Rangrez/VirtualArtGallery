using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.entity
{
    public class Gallery
    {
        //Properties
        public int GalleryId { get; set; }    //Primary Key
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int CuratorID { get; set; }    // Reference to ArtistID
        public string OpeningHours { get; set; }


        //Default Constructor
        public Gallery() { }

        //Parameterized Constructor
        public Gallery(int galleryId, string name, string description,
                        string location, int curatorID, string openingHours)
        {
            this.GalleryId = galleryId;
            this.Name = name;
            this.Description = description;
            this.Location = location;
            this.CuratorID = curatorID;
            this.OpeningHours = openingHours;
        }

        public override string ToString()
        {
            return $"GalleryId \t:\t{GalleryId}\nName \t\t:\t{Name}\nDescription \t:\t{Description}\nLocation \t:\t{Location}\ncuratorID \t:\t{CuratorID}\nOpeningHours \t:\t{OpeningHours}";
        }

    }
}
