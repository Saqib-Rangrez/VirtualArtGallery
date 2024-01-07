using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.entity
{
    public class FavoriteArtwork
    {
        //Properties
        public int UserID { get; set; }
        public int ArtworkID { get; set; }


        //Default Constructor
        public FavoriteArtwork() { }

        //Paramaterized Constructor
        public FavoriteArtwork(int UserID, int ArtworkID)
        {
            this.UserID = UserID;
            this.ArtworkID = ArtworkID;
        }
        public override string ToString()
        {
            return $"UserFavoriteArtwork [UserID={UserID}, ArtworkID={ArtworkID}]";
        }
    }
}
