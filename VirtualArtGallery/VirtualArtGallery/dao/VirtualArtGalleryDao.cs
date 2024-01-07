using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.entity;
using VirtualArtGallery.util;

namespace VirtualArtGallery.dao
{
    public class VirtualArtGalleryDao : IVirtualArtGallery
    {
        //Required Fields
        public List<Artwork> artworks;
        public List<FavoriteArtwork> favoriteArtwork;

        SqlCommand cmd = null;

        //Default Constructor
        public VirtualArtGalleryDao()
        {
            artworks = new List<Artwork>();
            favoriteArtwork = new List<FavoriteArtwork>();
            cmd = new SqlCommand();
        }

        public bool AddArtwork(Artwork artwork)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    cmd.CommandText = "INSERT INTO Artwork (Title, Description, CreationDate, Medium, ImageURL, ArtistID)" +
                        "values(@Title,@Discription,@creationDate,@medium,@imageURL,@artistId)";
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
                Console.WriteLine($"An error occurred during database operation: {ex.Message}");
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
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "DELETE FROM Artwork WHERE ArtworkID = @artworkId";
                    cmd.Parameters.AddWithValue("@artworkId", artworkID);

                    cmd.Connection = connection;
                    connection.Open();

                    int removeArtworkStatus = cmd.ExecuteNonQuery();
                    return removeArtworkStatus > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting artwork details: {ex.Message}");
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return false;
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "UPDATE Artwork SET Title = @Title, Description = @Description, CreationDate = @CreationDate, Medium = @Medium, ImageURL = @ImageURL, ArtistID = @ArtistID WHERE ArtworkID = @ArtworkID";
                    cmd.Parameters.AddWithValue("@Title", artwork.Title);
                    cmd.Parameters.AddWithValue("@Description", artwork.Description);
                    cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                    cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
                    cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                    cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);
                    cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);

                    cmd.Connection = connection;
                    connection.Open();

                    int updateStatus = cmd.ExecuteNonQuery();
                    return updateStatus > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting artwork details: {ex.Message}");
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return false;
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
                        }
                    }
                    if (artwork == null)
                    {
                        //throw new ArtWorkNotFoundException($"Artwork with ID {artworkID} not found.");
                    }
                    else
                    {
                        return artwork;
                    }
                }

            }
            /*catch (ArtWorkNotFoundException anf)
            {
                Console.WriteLine(anf.Message);
                throw;
            }*/
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting artwork details: {ex.Message}");
                return null;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return artwork;

        }

        public List<Artwork> GetUserFavoriteArtworks(int userId)
        {
            try
            {
                List<Artwork> FavoriteArtworkList = new List<Artwork>();
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    cmd.CommandText = @"SELECT A.* FROM Artwork A INNER JOIN FavoriteArtworks F ON A.ArtworkID = F.ArtworkID WHERE F.UserID = @UserID";
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred at Dao while getting artwork details: {ex.Message}");
            }
            finally
            {
                cmd.Parameters.Clear();
            }

            return null;
        }
        
        public bool AddArtworkToFavorite(int userId, int artworkId)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    cmd.CommandText = "Insert into FavoriteArtworks(UserID, ArtworkID) VALUES (@UserID, @ArtworkID)";
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@ArtworkID", artworkId);

                    cmd.Connection = connection;
                    connection.Open();

                    int addFavoriteStatus = cmd.ExecuteNonQuery();
                    return (addFavoriteStatus > 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during database operation: {ex.Message}");
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return false;            
        }

        public bool RemoveArtworkFromFavorite(int userId, int artworkId)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "DELETE FROM FavoriteArtworks WHERE UserID = @userId AND ArtworkID = @artworkId";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@artworkId", artworkId);

                    cmd.Connection = connection;
                    connection.Open();

                    int removeFavoriteStatus = cmd.ExecuteNonQuery();
                    return removeFavoriteStatus > 0;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while getting artwork details: {ex.Message}");
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return false;
            
        }

        public List<Artwork> SearchArtworks(string keyword)
        {
            List<Artwork> searchResults = new List<Artwork>();
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SELECT * FROM Artwork WHERE Title LIKE @keyword OR Description LIKE @keyword";
                    cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                    cmd.Connection = connection;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
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
                }

                return searchResults;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while getting artwork details: {ex.Message}");
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return null;
        }
                
        public Artwork ArtworkDetails()
        {
            try
            {
                Console.WriteLine("Enter Title:");
                string title = Console.ReadLine();

                Console.WriteLine("Enter Description:");
                string description = Console.ReadLine();

                Console.WriteLine("Enter Creation Date (yyyy-MM-dd):");
                DateTime creationDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);

                Console.WriteLine("Enter Medium:");
                string medium = Console.ReadLine();

                Console.WriteLine("Enter Image URL:");
                string imageURL = Console.ReadLine();

                Console.WriteLine("Enter Artist ID:");
                int artistID = int.Parse(Console.ReadLine());

                return new Artwork(title, description, creationDate, medium, imageURL, artistID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
}
