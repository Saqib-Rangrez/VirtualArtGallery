using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.entity
{
    public class ArtworkGallery
    {
        //Properties
        public int ArtworkID { get; set; }
        public int GalleryID { get; set; }


        //Default constructor
        public ArtworkGallery() { }

        //Parameterized constructor
        public ArtworkGallery(int artworkID, int galleryID)
        {
            this.ArtworkID = artworkID;
            this.GalleryID = galleryID;
        }

        public override string ToString()
        {
            return $"ArtworkGallery [ArtworkID={ArtworkID}, GalleryID={GalleryID}]";
        }
    }
}
