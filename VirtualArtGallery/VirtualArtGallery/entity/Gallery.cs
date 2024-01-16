using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.entity
{
    public class Gallery
    {
        public int GalleryId { get; set; }  
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int CuratorID { get; set; }  
        public string OpeningHours { get; set; }


        public Gallery() { }

        public Gallery( string name, string description,
                        string location, int curatorID, string openingHours, [Optional] int galleryId)
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
            return $"Gallery ID \t:\t{GalleryId}\nName \t\t:\t{Name}\nDescription \t:\t{Description}\nLocation \t:\t{Location}\nCurator ID \t:\t{CuratorID}\nOpening Hours \t:\t{OpeningHours}";
        }

    }
}
