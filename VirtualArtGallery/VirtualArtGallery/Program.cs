using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.dao;
using VirtualArtGallery.entity;
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
            Console.WriteLine("                                          VIRTUAL ART GALLERY CONSOLE APP        ");
            Console.WriteLine("======================================================================================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                   Welcome to the Virtual Art Gallery Console App!       ");
            Console.WriteLine("======================================================================================================================");
            Console.ResetColor();
            
            while (true)
            {
                Console.WriteLine("\nVIRTUAL ART GALLERY");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                Console.WriteLine("\nPlease enter your choice : ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Enter Username and Password");
                            string username = Console.ReadLine();
                            string password = Console.ReadLine();
                            bool LoginStatus = service.Login(username, password);
                            if (LoginStatus)
                            {
                                Console.WriteLine("Login SuccessFull");
                                obj.AfterLogin(username);
                            }
                            else
                            {
                               // throw new UserNotFoundException();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("User Not Found" + ex.Message);
                            Console.WriteLine();
                        }
                        break;

                    case "2":
                        Console.WriteLine("");
                        bool RegistrationStatus = service.Register();
                        if (RegistrationStatus)
                            Console.WriteLine("Registration Successfull!!");
                        else
                            Console.WriteLine("Registration Failed");
                        break;

                    case "3":
                        Console.WriteLine("Exiting the art gallery.");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number as displayed.");
                        break;
                }
            }

        }

        //******************************************************************************************
        //*********************************After Login Functionality********************************
        //******************************************************************************************


        public void AfterLogin(string username)
        {
            VirtualArtGalleryDao daoServices = new VirtualArtGalleryDao();
            IVirtualArtGalleryServices service = new VirtualArtGalleryServices();

            Console.WriteLine();
            Console.WriteLine("-------Welcome to Virtual Art Gallary Dashboard-------");
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine("1. Add Artwork");
                Console.WriteLine("2. Update Artwork");
                Console.WriteLine("3. Remove Artwork");
                Console.WriteLine("4. Get Artwork by Id");
                Console.WriteLine("5. Get User Favorite Artwork");
                Console.WriteLine("6. Browse Artwork");
                Console.WriteLine("7. View Galleries");
                Console.WriteLine("8. View Your Profile");
                Console.WriteLine("9. Logout");
                Console.WriteLine("10. Search Artwork");
                Console.WriteLine("11. Add Artwork to Favorites");
                Console.WriteLine("12. Remove Artwork from Favorites");
                Console.WriteLine();
                Console.WriteLine("Please enter your choice : ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Artwork artwork1 = new Artwork();
                        artwork1 = daoServices.ArtworkDetails();
                        Console.WriteLine();
                        bool status1 = daoServices.AddArtwork(artwork1);
                        if (status1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("╔══════════════════════════╗");
                            Console.WriteLine("║  Artwork Added Successfully  ║");
                            Console.WriteLine("╚══════════════════════════╝");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔══════════════════════════╗");
                            Console.WriteLine("║  Failed to add Artowork in Db  ║");
                            Console.WriteLine("╚══════════════════════════╝");
                            Console.ResetColor();
                        }
                            Console.WriteLine("Artwork Not Added in the Database");
                        break;
                    case "2":
                        try
                        {
                            Artwork artwork2 = new Artwork();
                            
                            artwork2 = daoServices.ArtworkDetails();
                            Console.WriteLine("Enter artwork ID");
                            artwork2.ArtworkID = int.Parse(Console.ReadLine());
                            Console.WriteLine(artwork2);
                            bool status2 = daoServices.UpdateArtwork(artwork2);
                            if (status2)
                            {
                                Console.ForegroundColor = ConsoleColor.Green; 
                                Console.WriteLine("╔══════════════════════════╗");
                                Console.WriteLine("║  Artwork Update Successful  ║");
                                Console.WriteLine("╚══════════════════════════╝");
                                Console.ResetColor(); 
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red; 
                                Console.WriteLine("╔══════════════════════════╗");
                                Console.WriteLine("║   Artwork Update Failed   ║");
                                Console.WriteLine("╚══════════════════════════╝");
                                Console.ResetColor(); 
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Artwork not found: {ex.Message}");
                        }
                        break;
                    case "3":
                        Console.WriteLine();
                        Console.WriteLine("Enter artwork ID to remove");
                        int id3 = int.Parse(Console.ReadLine());
                        bool status3 = daoServices.RemoveArtwork(id3);
                        if (status3)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("╔══════════════════════════╗");
                            Console.WriteLine("║   Artwork Removed Successfully   ║");
                            Console.WriteLine("╚══════════════════════════╝");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔══════════════════════════╗");
                            Console.WriteLine("║   Failed to Remove Artwork   ║");
                            Console.WriteLine("╚══════════════════════════╝");
                            Console.ResetColor();
                        }
                        break;
                    case "4":
                        try
                        {
                            Console.WriteLine();
                            Console.WriteLine("Enter Artwork Id");
                            int id4 = int.Parse(Console.ReadLine());
                            Artwork artwork4 = daoServices.GetArtworkByID(id4);
                            if (artwork4 != null)
                            {
                                Console.WriteLine(artwork4);
                            }
                            else
                                Console.WriteLine("Artwork Not Found");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Artwork not found: {ex.Message}");
                        }

                        break;
                    case "5":
                        Console.WriteLine();
                        List<Artwork> list = new List<Artwork>();
                        Console.WriteLine("Enter User ID");
                        int id = int.Parse(Console.ReadLine());
                        list = daoServices.GetUserFavoriteArtworks(id);
                        if (list != null)
                        {
                            foreach (Artwork art in list)
                            {
                                Console.WriteLine(art.ToString());
                                Console.WriteLine();
                            }
                        }
                        else
                            Console.WriteLine("Not Found");

                        break;
                    case "6":
                        Console.WriteLine("ArtworkID Title                  Description                                   CreationDate       Medium                 ImageURL                                           ArtistID");
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                        List<Artwork> artworkList = service.BrowseArtwork();
                        if (artworkList != null)
                        {
                            foreach (Artwork artwork in artworkList)
                            {
                                Console.WriteLine(artwork);
                                Console.WriteLine();
                            }
                        }
                        break;
                    case "7":
                        Console.WriteLine("GalleryId  Name                     Description                                  Location                        CuratorID    OpeningHours");
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");

                        List<Gallery> galleryList = service.ViewGalleries();
                        if (galleryList != null)
                        {
                            foreach (Gallery gallery in galleryList)
                            {
                                Console.WriteLine(gallery);
                                Console.WriteLine();
                            }
                        }
                        break;
                    case "8":
                        Users userProfile = service.GetUserProfile(username);

                        if (userProfile != null)
                        {
                            Console.WriteLine(userProfile);
                        }
                        break;
                    case "9":
                        if (service.Logout())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("╔══════════════════════════╗");
                            Console.WriteLine("║   Logout successfully....   ║");
                            Console.WriteLine("╚══════════════════════════╝");
                            Console.ResetColor();
                            return;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔══════════════════════════╗");
                            Console.WriteLine("║   Logout failed. Please try again   ║");
                            Console.WriteLine("╚══════════════════════════╝");
                            Console.ResetColor();
                            break;
                        }
                    case "10":
                        try
                        {
                            string keyword;
                            Console.WriteLine("Enter Artwork you want to search");
                            keyword = Console.ReadLine();
                            List<Artwork> artworks = daoServices.SearchArtworks(keyword);
                            Console.WriteLine("");
                            if (artworks != null)
                            {
                                foreach(Artwork artwork in artworks)
                                {
                                    Console.WriteLine($"\n{artwork.ToString()}" );
                                }
                            }
                        }
                        catch (Exception ex) {
                            Console.WriteLine($"Artwork not found: {ex.Message}");
                        }break;
                    case "11":
                        try
                        {
                            Console.WriteLine("Enter Artwork ID to add to favorites:");
                            if (int.TryParse(Console.ReadLine(), out int artworkId))
                            {
                                Console.WriteLine("Enter User ID:");
                                if (int.TryParse(Console.ReadLine(), out int userId))
                                {
                                    bool addToFavoritesStatus = daoServices.AddArtworkToFavorite(userId, artworkId);

                                    if (addToFavoritesStatus)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("╔══════════════════════════╗");
                                        Console.WriteLine("║   Artwork added to favorites successfully  ║");
                                        Console.WriteLine("╚══════════════════════════╝");
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("╔══════════════════════════╗");
                                        Console.WriteLine("║   Failed to add artwork to favorites   ║");
                                        Console.WriteLine("╚══════════════════════════╝");
                                        Console.ResetColor();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid User ID. Please enter a valid numeric ID.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Artwork ID. Please enter a valid numeric ID.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Artwork not found: {ex.Message}");
                        }
                        break;
                    case "12":
                        try
                        {
                            Console.WriteLine("Enter Artwork ID to remove from favorites:");
                            if (int.TryParse(Console.ReadLine(), out int artworkId))
                            {
                                Console.WriteLine("Enter User ID:");
                                if (int.TryParse(Console.ReadLine(), out int userId))
                                {
                                    bool removeFromFavoritesStatus = daoServices.RemoveArtworkFromFavorite(userId, artworkId);

                                    if (removeFromFavoritesStatus)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("╔══════════════════════════╗");
                                        Console.WriteLine("║   Artwork removed from favorites successfully  ║");
                                        Console.WriteLine("╚══════════════════════════╝");
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("╔══════════════════════════╗");
                                        Console.WriteLine("║   Failed to remove artwork to favorites  ║");
                                        Console.WriteLine("╚══════════════════════════╝");
                                        Console.ResetColor();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid User ID. Please enter a valid numeric ID.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Artwork ID. Please enter a valid numeric ID.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Artwork not found: {ex.Message}");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 9.");
                        break;
                }
            }
        }

    }
}
