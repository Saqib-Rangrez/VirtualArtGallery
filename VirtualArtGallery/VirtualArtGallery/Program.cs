using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.dao;
using VirtualArtGallery.entity;
using VirtualArtGallery.Exceptions;
using VirtualArtGallery.services;

namespace VirtualArtGallery
{
    public class MainModule
    {
        public static void Main(string[] args)
        {
            IVirtualArtGalleryServices service = new VirtualArtGalleryServices();
            MainModule obj = new MainModule();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("======================================================================================================================");
            Console.WriteLine("                                             VIRTUAL ART GALLERY APP        ");
            Console.WriteLine("======================================================================================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                   Welcome to the Virtual Art Gallery Console App!       ");
            Console.WriteLine("======================================================================================================================");
            Console.ResetColor();
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nVIRTUAL ART GALLERY - Menu");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("3. Exit");
                Console.ResetColor();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nPlease enter your choice : ");
                Console.ResetColor();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.WriteLine("");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Enter Username and Password");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("\nUsername : ");
                            Console.ResetColor();
                            string username = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("Password : ");
                            Console.ResetColor();
                            string password = Console.ReadLine();
                            try
                            {
                                Users LoginStatus = service.Login(username, password);
                                if (LoginStatus != null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("╔══════════════════════════╗");
                                    Console.WriteLine("     Login SuccessFull!!   ");
                                    Console.WriteLine("╚══════════════════════════╝");
                                    Console.ResetColor();
                                    obj.AfterLogin(LoginStatus);
                                }
                            }                            
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("╔═════════════════════════════════════════╗");
                                Console.WriteLine($"    {ex.Message}  ");
                                Console.WriteLine("╚═════════════════════════════════════════╝");
                                Console.ResetColor();
                            }                            
                        }
                        catch (Exception ex)
                        {                            
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("\n"+ex.Message);
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                        break;
                    case "2":
                        Console.WriteLine("");
                        bool RegistrationStatus = service.Register();
                        if (RegistrationStatus)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("╔══════════════════════════════╗");
                            Console.WriteLine("   Registration Successfull!!   ");
                            Console.WriteLine("╚══════════════════════════════╝");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔════════════════════════════════════╗");
                            Console.WriteLine("   Registration Failed, Try again!!  ");
                            Console.WriteLine("╚════════════════════════════════════╝");
                            Console.ResetColor();
                        }
                        break;
                    case "3":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("╔══════════════════════════════╗");
                        Console.WriteLine("   Exiting the art gallery...   ");
                        Console.WriteLine("╚══════════════════════════════╝");
                        Console.ResetColor();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("╔═════════════════════════════════════════════════════╗");
                        Console.WriteLine("   Invalid choice. Please enter a number as displayed ");
                        Console.WriteLine("╚═════════════════════════════════════════════════════╝");
                        Console.ResetColor();
                        break;
                }
            }

        }

        //*********************************After Login Functionality*********************************************
        public void AfterLogin(Users user)
        {
            VirtualArtGalleryDao daoServices = new VirtualArtGalleryDao();
            IVirtualArtGalleryServices service = new VirtualArtGalleryServices();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("═════════════════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------Welcome to Virtual Art Gallary Dashboard--------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("═════════════════════════════════════════════════════════════════════");
            Console.ResetColor();
            while (true)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. Add Artwork");
                Console.WriteLine("2. Update Artwork");
                Console.WriteLine("3. Remove Artwork");
                Console.WriteLine("4. Get Artwork By Id");
                Console.WriteLine("5. Get User Favorite Artwork");
                Console.WriteLine("6. Browse Artwork");
                Console.WriteLine("7. View Galleries");
                Console.WriteLine("8. Search Artwork");
                Console.WriteLine("9. Add Artwork to Favorites");
                Console.WriteLine("10. Remove Artwork from Favorites");
                Console.WriteLine("11. Add Gallery");
                Console.WriteLine("12. Update Gallery");
                Console.WriteLine("13. Remove Gallery");
                Console.WriteLine("14. Get Gallery By Id");
                Console.WriteLine("15. Search Gallery");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("16. View Your Profile");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("17. Logout");
                Console.ResetColor();
                Console.WriteLine();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Please enter your choice : ");
                Console.ResetColor();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Artwork artworkToAdd = new Artwork();
                        bool addStatus;
                        artworkToAdd = daoServices.ArtworkDetails();
                        Console.WriteLine();
                        if (artworkToAdd != null)
                        {
                            addStatus = daoServices.AddArtwork(artworkToAdd);

                            if (addStatus)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("╔══════════════════════════════╗");
                                Console.WriteLine("   Artwork Added Successfully   ");
                                Console.WriteLine("╚══════════════════════════════╝");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("╔════════════════════════════════╗");
                                Console.WriteLine("      Failed to add Artowork     ");
                                Console.WriteLine("╚════════════════════════════════╝");
                                Console.ResetColor();
                            }
                        }
                        break;
                    case "2":
                        try
                        {
                            Artwork artworkToUpdate;
                            bool updateStatus;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("\nEnter artwork ID to Update");
                            Console.Write("Artwork ID : ");
                            Console.ResetColor();
                            int artworkIdToUpdate = int.Parse(Console.ReadLine());
                            artworkToUpdate = daoServices.ArtworkDetails();
                            artworkToUpdate.ArtworkID = artworkIdToUpdate;
                            if (artworkToUpdate != null)
                            {
                                updateStatus = daoServices.UpdateArtwork(artworkToUpdate);
                                if (updateStatus)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("╔═════════════════════════════════╗");
                                    Console.WriteLine("    Artwork Updated Successfully  ");
                                    Console.WriteLine("╚═════════════════════════════════╝");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("╔══════════════════════════╗");
                                    Console.WriteLine("    Artwork Update Failed  ");
                                    Console.WriteLine("╚══════════════════════════╝");
                                    Console.ResetColor();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
                            Console.WriteLine($"     {ex.Message}   ");
                            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.WriteLine();
                        }                                         
                    break;
                    case "3":
                        try
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Enter artwork ID to remove");
                            Console.Write("Artwork ID : ");
                            Console.ResetColor();
                            int id = int.Parse(Console.ReadLine());
                            bool removeStatus = daoServices.RemoveArtwork(id);
                            if (removeStatus)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("╔══════════════════════════════════╗");
                                Console.WriteLine("    Artwork Removed Successfully   ");
                                Console.WriteLine("╚══════════════════════════════════╝");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("╔═════════════════════════════╗");
                                Console.WriteLine("    Failed to Remove Artwork  ");
                                Console.WriteLine("╚═════════════════════════════╝");
                                Console.ResetColor();
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
                            Console.WriteLine($"        {ex.Message}");
                            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.WriteLine();
                        }                        
                        break;
                    case "4":
                        try
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Enter Artwork Id");
                            Console.Write("Artwork ID : ");
                            Console.ResetColor();
                            int artworkId = int.Parse(Console.ReadLine());
                            Artwork artworkById = daoServices.GetArtworkByID(artworkId);
                            if (artworkById != null)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════╗");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("\t\t\t╔════════════════════════════════════════╗");
                                Console.WriteLine($"\t\t\t     Artowork of ArtworkId : {artworkId}       ");
                                Console.WriteLine("\t\t\t╚════════════════════════════════════════╝");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(artworkById);
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("\n╚═══════════════════════════════════════════════════════════════════════════════════════╝");
                                Console.ResetColor();
                            }                            
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
                            Console.WriteLine($"         {ex.Message}   ");
                            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                        break;
                    case "5":
                        Console.WriteLine();
                        List<Artwork> artwroks = new List<Artwork>();
                        artwroks = daoServices.GetUserFavoriteArtworks(user.UserId);
                        if (artwroks != null)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════╗");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("\t\t\t╔════════════════════════════════════════╗");
                            Console.WriteLine("\t\t\t         User Favorite Artworks           ");
                            Console.WriteLine("\t\t\t╚════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("---------------------------------------------------------------------------------------");
                            Console.ResetColor();
                            foreach (Artwork artwork in artwroks)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(artwork.ToString());
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("--------------------------------------------------------------------------------------");
                                Console.ResetColor();
                            }
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("\n╚═══════════════════════════════════════════════════════════════════════════════════════╝");
                            Console.ResetColor();
                        }                        
                        break;
                    case "6":

                        List<Artwork> artworkList = daoServices.BrowseArtwork();
                        if (artworkList != null)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════╗");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("\t\t╔════════════════════════════════════════╗");
                            Console.WriteLine("\t\t        All Available Artworks            ");
                            Console.WriteLine("\t\t╚════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("-------------------------------------------------------------------------");
                            Console.ResetColor();
                            foreach (Artwork artwork in artworkList)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(artwork.ToString());
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("-----------------------------------------------------------------------");
                                Console.ResetColor();
                            }
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("\n╚════════════════════════════════════════════════════════════════════════╝");
                            Console.ResetColor();
                        }
                        break;
                    case "7":                        
                        List<Gallery> galleryList = daoServices.ViewGalleries();
                        if (galleryList != null)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════╗");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("\t\t╔════════════════════════════════════════╗");
                            Console.WriteLine("\t\t          All Available Galleries         ");
                            Console.WriteLine("\t\t╚════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("-----------------------------------------------------------------------------");
                            Console.ResetColor();
                            foreach (Gallery gallery in galleryList)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(gallery);
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("----------------------------------------------------------------------------");
                                Console.ResetColor();
                            }
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("\n╚═════════════════════════════════════════════════════════════════════════════╝");
                            Console.ResetColor();
                        }
                        break;
                    case "8":
                        try
                        {
                            string keyword;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("\nEnter Artwork you want to search");
                            Console.Write("Search Keyword : ");
                            Console.ResetColor();
                            keyword = Console.ReadLine();
                            List<Artwork> artworks = daoServices.SearchArtworks(keyword);
                            Console.WriteLine("");
                            if (artworks != null)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("\t\t╔═════════════════════════════════════════════════════════════════╗");
                                Console.WriteLine($"\t\t        All Available Artwork with keyword {keyword}              ");
                                Console.WriteLine("\t\t╚═════════════════════════════════════════════════════════════════╝");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                                Console.ResetColor();
                                foreach (Artwork artwork in artworks)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"\n{artwork.ToString()}");
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("---------------------------------------------------------------------------------------------------");
                                    Console.ResetColor();
                                }
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("\n╚═════════════════════════════════════════════════════════════════════════════════════════════════════╝");
                                Console.ResetColor();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
                            Console.WriteLine($"     {ex.Message}   ");
                            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                        break;
                    case "9":
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Enter Artwork ID to Add to favorites:");
                            Console.Write("ArtworkID : ");
                            Console.ResetColor();
                            if (int.TryParse(Console.ReadLine(), out int artworkIdToAdd))
                            {
                                bool addToFavoritesStatus = daoServices.AddArtworkToFavorite(user.UserId, artworkIdToAdd);

                                if (addToFavoritesStatus)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("╔════════════════════════════════════════════╗");
                                    Console.WriteLine("    Artwork added to favorites successfully  ");
                                    Console.WriteLine("╚════════════════════════════════════════════╝");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("╔═══════════════════════════════════════╗");
                                    Console.WriteLine("    Failed to add artwork to favorites  ");
                                    Console.WriteLine("╚═══════════════════════════════════════╝");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
                                Console.WriteLine("    Invalid Artwork ID. Please enter a valid numeric ID  ");
                                Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
                                Console.ResetColor();
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
                            Console.WriteLine($"     {ex.Message}   ");
                            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.WriteLine();
                        }                                        
                        break;
                    case "10":
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Enter Artwork ID to remove from favorites:");
                            Console.Write("ArtworkID : ");
                            Console.ResetColor();
                            if (int.TryParse(Console.ReadLine(), out int artworkIdToRemove))
                            {
                                bool removeFromFavoritesStatus = daoServices.RemoveArtworkFromFavorite(user.UserId, artworkIdToRemove);

                                if (removeFromFavoritesStatus)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("╔════════════════════════════════════════════════╗");
                                    Console.WriteLine("   Artwork removed from favorites successfully    ");
                                    Console.WriteLine("╚════════════════════════════════════════════════╝");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("╔═══════════════════════════════════════════╗");
                                    Console.WriteLine("    Failed to remove artwork from favorites    ");
                                    Console.WriteLine("╚═══════════════════════════════════════════╝");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
                                Console.WriteLine("    Invalid Artwork ID. Please enter a valid numeric ID  ");
                                Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
                                Console.ResetColor();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
                            Console.WriteLine($"     {ex.Message}   ");
                            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                                        
                        break;
                    case "11":
                        Gallery galleryToAdd = new Gallery();
                        galleryToAdd = daoServices.GalleryDetails();
                        Console.WriteLine();
                        bool addGalleryStatus = daoServices.AddGallery(galleryToAdd);
                        if (addGalleryStatus)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("╔══════════════════════════════╗");
                            Console.WriteLine("   Gallery Added Successfully   ");
                            Console.WriteLine("╚══════════════════════════════╝");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔═════════════════════════════════════════╗");
                            Console.WriteLine("      Failed to add Gallery! Try again     ");
                            Console.WriteLine("╚═════════════════════════════════════════╝");
                            Console.ResetColor();
                        }
                        break;
                    case "12":
                        try
                        {
                            Gallery galleryToUpdate = new Gallery();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("\nEnter Gallery Id to Update");
                            Console.Write("Gallery ID : ");
                            Console.ResetColor();
                            int galleryId = Convert.ToInt32(Console.ReadLine());
                            galleryToUpdate = daoServices.GalleryDetails();
                            galleryToUpdate.GalleryId = galleryId;
                            Console.WriteLine();
                            bool updateGalleryStatus = daoServices.UpdateGallery(galleryToUpdate);
                            if (updateGalleryStatus)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("╔══════════════════════════════╗");
                                Console.WriteLine("   Gallery Updated Successfully ");
                                Console.WriteLine("╚══════════════════════════════╝");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("╔════════════════════════════════════════════╗");
                                Console.WriteLine("      Failed to update Gallery! Try again     ");
                                Console.WriteLine("╚════════════════════════════════════════════╝");
                                Console.ResetColor();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
                            Console.WriteLine($"     {ex.Message}   ");
                            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.WriteLine();
                        }                        
                        break;
                    case "13":
                        try
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Enter Gallery ID to remove");
                            Console.Write("Gallery ID : ");
                            Console.ResetColor();
                            int galleryIdToRemove = int.Parse(Console.ReadLine());
                            bool removeGalleryStatus = daoServices.RemoveGallery(galleryIdToRemove);
                            if (removeGalleryStatus)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("╔══════════════════════════════════╗");
                                Console.WriteLine("    Gallery Removed Successfully    ");
                                Console.WriteLine("╚══════════════════════════════════╝");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("╔═════════════════════════════╗");
                                Console.WriteLine("    Failed to Remove Gallery   ");
                                Console.WriteLine("╚═════════════════════════════╝");
                                Console.ResetColor();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
                            Console.WriteLine($"     {ex.Message}   ");
                            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.WriteLine();
                        }                        
                        break;
                    case "14":
                        try
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Enter Gallery Id");
                            Console.Write("Gallery ID : ");
                            Console.ResetColor();
                            int galleryIdToSearch = int.Parse(Console.ReadLine());
                            Gallery galleryById = daoServices.GetGalleryById(galleryIdToSearch);
                            if (galleryById != null)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════╗");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("\t\t\t╔════════════════════════════════════════╗");
                                Console.WriteLine($"\t\t\t      Gallery of GalleryID : {galleryIdToSearch}        ");
                                Console.WriteLine("\t\t\t╚════════════════════════════════════════╝");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(galleryById);
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("\n╚═══════════════════════════════════════════════════════════════════════════════════════╝");
                                Console.ResetColor();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
                            Console.WriteLine($"     {ex.Message}   ");
                            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                        break;
                    case "15":
                        try
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Enter Keyword to Search");
                            Console.Write("Search Keyword : ");
                            Console.ResetColor();
                            string galleryToSearch = Console.ReadLine();
                            List<Gallery> galleryByKeyword = daoServices.SearchGallery(galleryToSearch);
                            if (galleryByKeyword != null)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════════════════╗");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("\t\t╔══════════════════════════════════════════════════════════════╗");
                                Console.WriteLine($"\t\t             Gallery with Keyword : {galleryToSearch}         ");
                                Console.WriteLine("\t\t╚══════════════════════════════════════════════════════════════╝");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("---------------------------------------------------------------------------------------------------");
                                Console.ResetColor();
                                foreach (Gallery gallery in galleryByKeyword)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine(gallery);
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("--------------------------------------------------------------------------------------------------");
                                    Console.ResetColor();
                                }
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("\n╚═══════════════════════════════════════════════════════════════════════════════════════════════════╝");
                                Console.ResetColor();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
                            Console.WriteLine($"     {ex.Message}   ");
                            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                        break;
                    case "16":
                        Users userProfile = service.GetUserProfile(user.UserName);

                        if (userProfile != null)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════╗");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("\t\t╔════════════════════════════╗");
                            Console.WriteLine("\t\t         User Deatils         ");
                            Console.WriteLine("\t\t╚════════════════════════════╝");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(userProfile);
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("\n╚════════════════════════════════════════════════════════════════════════╝");
                            Console.ResetColor();
                        }
                        break;
                    case "17":
                        if (service.Logout())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("╔══════════════════════════════╗");
                            Console.WriteLine("     Logout successfully....    ");
                            Console.WriteLine("╚══════════════════════════════╝");
                            Console.ResetColor();
                            return;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔════════════════════════════════════╗");
                            Console.WriteLine("    Logout failed. Please try again   ");
                            Console.WriteLine("╚════════════════════════════════════╝");
                            Console.ResetColor();
                            break;
                        }
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
                        Console.WriteLine("    Invalid choice. Please enter a number between 1 and 17    ");
                        Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
                        Console.ResetColor();
                        break;
                }
            }
        }

    }
}
