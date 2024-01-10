using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.entity;

namespace VirtualArtGallery.services
{
    public interface IVirtualArtGalleryServices
    {
        bool Register();
        Users Login(string username, string password);
        bool Logout();
        Users GetUserProfile(string username);
        
    }
}
