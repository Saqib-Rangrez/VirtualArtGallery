using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.entity;

namespace VirtualArtGallery.dao
{
    public interface IVirtualArtGallery
    {
        //Artwork Management
        Boolean AddArtwork(Artwork artwork);
        Boolean UpdateArtwork(Artwork artwork);
        Boolean RemoveArtwork(int artworkID);
        Artwork GetArtworkByID(int artworkID);
        List<Artwork> SearchArtworks(String keyword);
        List<Artwork> BrowseArtwork();
        List<Artist> GetAllArtist();

        //Gallery Management
        List<Gallery> ViewGalleries();
        Boolean AddGallery(Gallery gallery);
        Boolean UpdateGallery(Gallery gallery);
        Boolean RemoveGallery(int galleryID);
        Gallery GetGalleryById(int galleryID);
        List<Gallery> SearchGallery(String keyword);

        //User Favorites
        Boolean AddArtworkToFavorite(int userId, int artworkId);
        Boolean RemoveArtworkFromFavorite(int userId, int artworkId);
        List<Artwork> GetUserFavoriteArtworks(int userId);
    }
}
