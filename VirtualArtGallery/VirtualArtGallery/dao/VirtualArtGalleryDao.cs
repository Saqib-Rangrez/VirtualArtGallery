using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.entity;
using VirtualArtGallery.exception;
using VirtualArtGallery.Exceptions;
using VirtualArtGallery.util;

namespace VirtualArtGallery.dao
{
    public class VirtualArtGalleryDao : IVirtualArtGallery
    {
        //Required Fields
        SqlCommand cmd = null;

        //Default Constructor
        public VirtualArtGalleryDao()
        {            
            cmd = new SqlCommand();
        }



        //Artwork Management Methods
        public bool AddArtwork(Artwork artwork)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    cmd.CommandText = "INSERT INTO Artwork (Title, Description, CreationDate, Medium, ImageURL, ArtistID)" +
                        "VALUES (@Title,@Discription,@creationDate,@medium,@imageURL,@artistId)";
                    cmd.Parameters.AddWithValue("@Title", artwork.Title);
                    cmd.Parameters.AddWithValue("@Discription", artwork.Description);
                    cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                    cmd.Parameters.AddWithValue("@medium", artwork.Medium);
                    cmd.Parameters.AddWithValue("@imageURL", artwork.ImageURL);
                    cmd.Parameters.AddWithValue("@artistId", artwork.ArtistID);

                    cmd.Connection = connection;
                    connection.Open();

                    int addArtworkStatus = cmd.ExecuteNonQuery();
                    return addArtworkStatus > 0;
                }
            }catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return false;
        }

        public bool RemoveArtwork(int artworkID)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {                   
                    cmd.CommandText = "SELECT * FROM Artwork WHERE ArtworkID = @ArtworkID";
                    cmd.Parameters.AddWithValue("@ArtworkID", artworkID);
                    cmd.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DELETE FROM Artwork WHERE ArtworkID = @artworkId";
                        cmd.Parameters.AddWithValue("@ArtworkID", artworkID);

                        int removeArtworkStatus = cmd.ExecuteNonQuery();
                        Console.WriteLine(removeArtworkStatus);
                        return removeArtworkStatus > 0;
                    }
                    else
                    {
                        throw new ArtWorkNotFoundException($"Artwork with ID : {artworkID} not found.");
                    }
                }
            }catch(ArtWorkNotFoundException anf)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"        {anf.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return false;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return false;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            if (artwork != null)
            {
                try
                {
                    using (SqlConnection connection = DBConnUtil.GetConnection())
                    {
                        cmd.CommandText = "SELECT * FROM Artwork WHERE ArtworkID = @ArtworkID";
                        cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
                        cmd.Connection = connection;
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            reader.Close();
                            cmd.Parameters.Clear();
                            cmd.CommandText = "UPDATE Artwork SET Title = @Title, Description = @Description, CreationDate = @CreationDate, " +
                            "Medium = @Medium, ImageURL = @ImageURL, ArtistID = @ArtistID WHERE ArtworkID = @ArtworkID";
                            cmd.Parameters.AddWithValue("@Title", artwork.Title);
                            cmd.Parameters.AddWithValue("@Description", artwork.Description);
                            cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                            cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
                            cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                            cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);
                            cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);

                            int updateStatus = cmd.ExecuteNonQuery();
                            return updateStatus > 0;
                        }
                        else
                        {
                            throw new ArtWorkNotFoundException($"Artwork with ID : {artwork.ArtworkID} not found.");
                        }
                    }
                }
                catch (ArtWorkNotFoundException anf)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine($"                    {anf.Message}  ");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                    Console.ResetColor();
                    return false;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                    Console.ResetColor();
                    return false;
                }
                finally
                {
                    cmd.Parameters.Clear();
                }
            }
            else return false;            
        }

        public Artwork GetArtworkByID(int artworkID)
        {
            Artwork artwork = new Artwork();
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    cmd.CommandText = "Select * from Artwork where artworkId=@artworkID";
                    cmd.Parameters.AddWithValue("@artworkId", artworkID);

                    cmd.Connection = connection;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            artwork = new Artwork
                            {
                                ArtworkID = Convert.ToInt32(reader["ArtworkID"]),
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                                Medium = reader["Medium"].ToString(),
                                ImageURL = reader["ImageURL"].ToString(),
                                ArtistID = Convert.ToInt32(reader["ArtistID"])
                            };
                            return artwork;
                        }else
                        {
                            throw new ArtWorkNotFoundException($"Artwork with ID {artworkID} not found.");
                        }
                    }                    
                }
            }
            catch (ArtWorkNotFoundException anf)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine($"         {anf.Message}            ");
                Console.WriteLine("╚══════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"  An error occurred during database operation at Login: {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
        }

        public List<Artwork> SearchArtworks(string keyword)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    cmd.CommandText = "SELECT * FROM Artwork WHERE Title LIKE @keyword OR Description LIKE @keyword";
                    cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                    cmd.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();                   
                    if (reader.Read())
                    {
                        reader.Close();
                        connection.Close();
                        connection.Open();                            
                        List<Artwork> searchResults = new List<Artwork>();
                        using(reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Artwork artwork = new Artwork
                                {
                                    ArtworkID = Convert.ToInt32(reader["ArtworkID"]),
                                    Title = reader["Title"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                                    Medium = reader["Medium"].ToString(),
                                    ImageURL = reader["ImageURL"].ToString(),
                                    ArtistID = Convert.ToInt32(reader["ArtistID"])
                                };

                                searchResults.Add(artwork);
                            }
                        }                        
                        return searchResults;
                    }else
                    {
                        throw new ArtWorkNotFoundException($"Artwork with keyword {keyword} not found.");
                    }                                        
                }
            }catch(ArtWorkNotFoundException anf)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine($"         {anf.Message}            ");
                Console.WriteLine("╚══════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
        }

        public List<Artwork> BrowseArtwork()
        {
            List<Artwork> artworks = new List<Artwork>();

            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Artwork";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Artwork artwork = new Artwork()
                                {
                                    ArtworkID = Convert.ToInt32(reader["ArtworkID"]),
                                    Title = reader["Title"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                                    Medium = reader["Medium"].ToString(),
                                    ImageURL = reader["ImageURL"].ToString(),
                                    ArtistID = Convert.ToInt32(reader["ArtistID"])
                                };
                                artworks.Add(artwork);
                            }
                        }

                    }
                }
                return artworks;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
        }

        public List<Artist> GetAllArtist()
        {
            try
            {
                List<Artist> artistList = new List<Artist>();

                SqlCommand cmd = new SqlCommand();
                SqlConnection connection = DBConnUtil.GetConnection();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Artist";

                connection.Open();

                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    while (data.Read())
                    {
                        artistList.Add(new Artist(
                             Convert.ToInt32(data["ArtistID"]),
                             data["Name"].ToString(),
                             data["Biography"].ToString(),
                             Convert.ToDateTime(data["BirthDate"]),
                             data["Nationality"].ToString(),
                             data["Website"].ToString(),
                             data["ContactInformation"].ToString()
                            ));
                    }
                }
                connection.Close();

                if (artistList.Count > 0)
                {
                    return artistList;
                }
                else
                { return null; }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting Artist details: {ex.Message}");
                return null;
            }
        }

        //User Favorites Methods
        public List<Artwork> GetUserFavoriteArtworks(int userId)
        {
            try
            {
                List<Artwork> FavoriteArtworkList = new List<Artwork>();
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    cmd.CommandText = "SELECT A.* FROM Artwork A INNER JOIN FavoriteArtworks F ON A.ArtworkID = F.ArtworkID WHERE F.UserID = @UserID";
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Close();
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Artwork temp = new Artwork
                                {
                                    ArtworkID = Convert.ToInt32(reader["ArtworkID"]),
                                    Title = reader["Title"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                                    Medium = reader["Medium"].ToString(),
                                    ImageURL = reader["ImageURL"].ToString(),
                                    ArtistID = Convert.ToInt32(reader["ArtistID"])
                                };
                                FavoriteArtworkList.Add(temp);
                            }
                        }                        
                        return FavoriteArtworkList;
                    }
                    else
                    {
                        throw new ArtWorkNotFoundException($"You have not added any artwork in favorites.");
                    }
                }
                
            }catch(ArtWorkNotFoundException anf)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"        {anf.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
        }
        
        public bool AddArtworkToFavorite(int userId, int artworkId)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    cmd.CommandText = "SELECT * FROM Artwork WHERE ArtworkID = @ArtworkID";
                    cmd.Parameters.AddWithValue("@ArtworkID", artworkId);
                    cmd.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        reader.Close();
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SELECT A.* FROM Artwork A INNER JOIN FavoriteArtworks F ON A.ArtworkID = F.ArtworkID " +
                            "WHERE F.UserID = @UserID AND A.ArtworkID = @ArtworkID";
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("ArtworkID", artworkId);
                        reader = cmd.ExecuteReader();
                        if(!reader.Read())
                        {
                            reader.Close();
                            cmd.Parameters.Clear();
                            cmd.CommandText = "Insert into FavoriteArtworks(UserID, ArtworkID) VALUES (@UserID, @ArtworkID)";
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.Parameters.AddWithValue("@ArtworkID", artworkId);

                            int addFavoriteStatus = cmd.ExecuteNonQuery();
                            return addFavoriteStatus > 0;
                        }
                        else
                        {
                            throw new ArtWorkNotFoundException($"Artwork with Artwork ID : {artworkId} already exist in your Favorites");
                        }                        
                    }
                    else
                    {
                        throw new ArtWorkNotFoundException($"Artwork with ID : {artworkId} not found.");
                    }
                }
            }
            catch (ArtWorkNotFoundException anf)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
                Console.WriteLine($"        {anf.Message}            ");
                Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return false;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return false;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
        }

        public bool RemoveArtworkFromFavorite(int userId, int artworkId)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {                  
                    cmd.CommandText = "SELECT * FROM Artwork WHERE ArtworkID = @ArtworkID";
                    cmd.Parameters.AddWithValue("@ArtworkID", artworkId);
                    cmd.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SELECT A.* FROM Artwork A INNER JOIN FavoriteArtworks F ON A.ArtworkID = F.ArtworkID " +
                            "WHERE F.UserID = @UserID AND A.ArtworkID = @ArtworkID";
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("ArtworkID", artworkId);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            reader.Close();
                            cmd.Parameters.Clear();
                            cmd.CommandText = "DELETE FROM FavoriteArtworks WHERE UserID = @userId AND ArtworkID = @artworkId";
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.Parameters.AddWithValue("@ArtworkID", artworkId);

                            int removeFavoriteStatus = cmd.ExecuteNonQuery();
                            Console.WriteLine(removeFavoriteStatus);
                            return removeFavoriteStatus > 0;
                        }
                        else
                        {
                            throw new ArtWorkNotFoundException($"Artwork with Artwork ID : {artworkId} not found in your Favorites");
                        }
                    }
                    else
                    {
                        throw new ArtWorkNotFoundException($"Artwork with ID : {artworkId} not found.");
                    }
                }
            }
            catch (ArtWorkNotFoundException anf)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
                Console.WriteLine($"         {anf.Message}            ");
                Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return false;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return false;
            }
            finally
            {
                cmd.Parameters.Clear();
            }            
        }

        


        //Gallery Management Methods
        public List<Gallery> ViewGalleries()
        {

            List<Gallery> list = new List<Gallery>();
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Gallery";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Gallery temp = new Gallery
                                {
                                    GalleryId = Convert.ToInt32(reader["galleryId"]),
                                    Name = reader["name"].ToString(),
                                    Description = reader["description"].ToString(),
                                    Location = reader["location"].ToString(),
                                    CuratorID = Convert.ToInt32(reader["curatorId"]),
                                    OpeningHours = reader["openingHours"].ToString()
                                };
                                list.Add(temp);
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
        }

        public Boolean AddGallery(Gallery gallery)
        {
            try
            {
                using(SqlConnection connection = DBConnUtil.GetConnection())
                {
                    cmd.CommandText = "INSERT INTO Gallery (Name, Description, Location, CuratorID, OpeningHours) " +
                        "Values (@Name,@Description,@Location,@CuratorID,@OpeningHours)";
                    cmd.Parameters.AddWithValue("@Name", gallery.Name);
                    cmd.Parameters.AddWithValue("@Description", gallery.Description);
                    cmd.Parameters.AddWithValue("@Location", gallery.Location);
                    cmd.Parameters.AddWithValue("@CuratorID", gallery.CuratorID);
                    cmd.Parameters.AddWithValue("@OpeningHours", gallery.OpeningHours);
                    cmd.Connection = connection;
                    connection.Open();

                    int addGalleryStatus = cmd.ExecuteNonQuery();

                    return addGalleryStatus > 0;
                }
            }catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return false;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
        }

        public bool UpdateGallery(Gallery gallery)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Gallery WHERE GalleryID = @GalleryID";
                    cmd.Parameters.AddWithValue("@GalleryID", gallery.GalleryId);
                    cmd.Connection = connection;
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        reader.Close();
                        connection.Close();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE Gallery SET Name = @Name, Description = @Description, Location = @Location, " +
                        "CuratorID = @CuratorID, OpeningHours = @OpeningHours WHERE GalleryID = @GalleryID";
                        cmd.Parameters.AddWithValue("@Name", gallery.Name);
                        cmd.Parameters.AddWithValue("@Description", gallery.Description);
                        cmd.Parameters.AddWithValue("@Location", gallery.Location);
                        cmd.Parameters.AddWithValue("@CuratorID", gallery.CuratorID);
                        cmd.Parameters.AddWithValue("@OpeningHours", gallery.OpeningHours);
                        cmd.Parameters.AddWithValue("@GalleryID", gallery.GalleryId);
                        cmd.Connection = connection;
                        connection.Open();

                        int updateGalleryStatus = cmd.ExecuteNonQuery();

                        return updateGalleryStatus > 0;
                    }
                    else
                    {
                        throw new GalleryNotFoundException($"Gallery with Gallery ID : {gallery.GalleryId} Not Found!");
                    }                    
                }
            }catch(GalleryNotFoundException gnf)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"             {gnf.Message}  ");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.ResetColor();
                return false;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return false;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
        }

        public bool RemoveGallery(int galleryID)
        {
            try
            {
                using(SqlConnection connection = DBConnUtil.GetConnection())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Gallery WHERE GalleryID = @GalleryID";
                    cmd.Parameters.AddWithValue("@GalleryID", galleryID);
                    cmd.Connection = connection;
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        connection.Close();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "DELETE FROM Gallery WHERE GalleryID = @GalleryID";
                        cmd.Parameters.AddWithValue("@GalleryID", galleryID);
                        cmd.Connection = connection;
                        connection.Open();

                        int galleryRemoveStatus = cmd.ExecuteNonQuery();

                        return galleryRemoveStatus > 0;
                    }
                    else
                    {
                        throw new GalleryNotFoundException($"Gallery with Gallery ID : {galleryID} Not Found!");
                    }
                }
            }
            catch (GalleryNotFoundException gnf)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"             {gnf.Message}  ");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.ResetColor();
                return false;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return false;
            }finally
            {
                cmd.Parameters.Clear();
            }
        }

        public Gallery GetGalleryById(int galleryID)
        {
            try
            {
                using(SqlConnection connection = DBConnUtil.GetConnection())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Gallery WHERE GalleryID = @GalleryID";
                    cmd.Parameters.AddWithValue("@GalleryID", galleryID);
                    cmd.Connection = connection;
                    connection.Open ();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Gallery gallery = new Gallery()
                            {
                                GalleryId = Convert.ToInt32(reader["GalleryID"]),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Location = reader["Location"].ToString(),
                                CuratorID = Convert.ToInt32(reader["CuratorID"]),
                                OpeningHours = reader["OpeningHours"].ToString()
                            };
                            return gallery;
                        }else
                        {
                            throw new GalleryNotFoundException($"Gallery with Gallery ID : {galleryID} Not Found!");
                        }
                    }
                }
            }
            catch (GalleryNotFoundException gnf)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"             {gnf.Message}  ");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }finally { 
                cmd.Parameters.Clear(); 
            }
        }

        public List<Gallery> SearchGallery(String keyword)
        {
            try
            {
                List<Gallery> galleries = new List<Gallery>();
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    cmd.CommandText = "SELECT * FROM Artwork WHERE Title LIKE @keyword OR Description LIKE @keyword";
                    cmd.CommandText = "SELECT * FROM Gallery WHERE Name LIKE @keyword OR Description LIKE @keyword";
                    cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");
                    cmd.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();                    
                    if(reader.HasRows)
                    {
                        reader.Close();                        
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Gallery gallery = new Gallery()
                                {
                                    GalleryId = Convert.ToInt32(reader["GalleryID"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Location = reader["Location"].ToString(),
                                    CuratorID = Convert.ToInt32(reader["CuratorID"]),
                                    OpeningHours = reader["OpeningHours"].ToString()
                                };
                                galleries.Add(gallery);
                            }
                        }                        
                        return galleries;
                    }
                    else
                    {
                        throw new GalleryNotFoundException($"Gallery with keyword : {keyword} Not Found!");
                    }
                }                
            }
            catch (GalleryNotFoundException gnf)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"             {gnf.Message}  ");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.ResetColor();
                return null;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during database operation : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
        }



        //Utility functions to get artwork and gallery details from users
        public Artwork ArtworkDetails()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("\nEnter Artist ID:");
                Console.ResetColor();
                int artistID = int.Parse(Console.ReadLine());

                bool isAvailable = CheckArtistID(artistID);

                if (!isAvailable)
                {
                    throw new Exception("Artist ID not Found");
                }

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter Title:");
                Console.ResetColor();
                string title = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter Description:");
                Console.ResetColor();
                string description = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter Creation Date (yyyy-mm-dd):");
                Console.ResetColor();
                DateTime creationDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter Medium:");
                Console.ResetColor();
                string medium = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter Image URL:");
                Console.ResetColor();
                string imageURL = Console.ReadLine();

                

                return new Artwork(title, description, creationDate, medium, imageURL, artistID);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"                      {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
        }
        
        public Gallery GalleryDetails()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("\nEnter Curator Id:");
                Console.ResetColor();
                int curatorId = Convert.ToInt32(Console.ReadLine());

                bool isAvailable = CheckArtistID(curatorId);

                if (!isAvailable)
                {
                    throw new Exception("Artist ID not Found");
                }

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter Gallry Name:");

                Console.ResetColor();
                string name = Console.ReadLine();                

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter Description:");
                Console.ResetColor();
                string description = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter Location:");
                Console.ResetColor();
                string location = Console.ReadLine();
                                
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter Opening Hours as TTAM to TTPM:");
                Console.ResetColor();
                string openingHours = Console.ReadLine();

                return new Gallery(name, description, location, curatorId, openingHours);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"                    {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
        }

        public bool CheckArtistID(int artistID)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Artist WHERE ArtistID = @ID";
                    cmd.Parameters.AddWithValue("@ID", artistID);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                    return false;
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }            
        }

    }
}
