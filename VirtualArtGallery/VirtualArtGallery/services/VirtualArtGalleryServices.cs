using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.entity;
using VirtualArtGallery.Exceptions;
using VirtualArtGallery.util;

namespace VirtualArtGallery.services
{
    public class VirtualArtGalleryServices : IVirtualArtGalleryServices
    {
        public Users Login(string username, string password)
        {
            try
            {
                using(SqlConnection connection = DBConnUtil.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT * FROM Users WHERE Username = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Users user = new Users()
                                {
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    UserName = reader["Username"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    UserId = Convert.ToInt32(reader["UserID"]),
                                    ProfilePicture = reader["ProfilePicture"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"])
                                };                                
                                
                                //Authentication
                                string storedPassword = reader["Password"].ToString();

                                if (storedPassword == password)
                                {
                                    return user;
                                }else
                                {
                                    throw new UserNotFoundException($"Password mismatch, Please enter correct password");
                                }
                            }
                            else
                            {
                                throw new UserNotFoundException($"User with username : {username} does not exist, Please register first");
                            }
                        }
                    }
                }
            }catch(UserNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔═════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"             {ex.Message}  ");
                Console.WriteLine("╚═════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"   An error occurred during database operation at Login: {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
        }

        public bool Register()
        {
            Users newUser = GetUsersDetails();
            if(newUser != null)
            {
                try
                {
                    using (SqlConnection connection = DBConnUtil.GetConnection())
                    {
                        connection.Open();
                        string insertQuery = "INSERT INTO Users (Username, Password, Email, FirstName, LastName, DateOfBirth, ProfilePicture) " +
                                             "VALUES (@Username, @Password, @Email, @FirstName, @LastName, @DateOfBirth, @ProfilePicture)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Username", newUser.UserName);
                            command.Parameters.AddWithValue("@Password", newUser.Password);
                            command.Parameters.AddWithValue("@Email", newUser.Email);
                            command.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                            command.Parameters.AddWithValue("@LastName", newUser.LastName);
                            command.Parameters.AddWithValue("@DateOfBirth", newUser.DateOfBirth);
                            command.Parameters.AddWithValue("@ProfilePicture", newUser.ProfilePicture);

                            int rowsAffected = command.ExecuteNonQuery();

                            return rowsAffected > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine($"   An error occurred during database operation : {ex.Message}  ");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                    Console.ResetColor();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Users GetUsersDetails()
        {
            Users newUser;
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("User Registration:");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Username: ");
                Console.ResetColor();
                string username = Console.ReadLine();

                //Validation
                bool isUsernameAvailable = CheckUsername(username);
                if (!isUsernameAvailable)
                {
                    throw new Exception("Username already exist, please enter unique username");
                }


                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Password: ");
                Console.ResetColor();
                string password = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Email: ");
                Console.ResetColor();
                string email = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("First Name: ");
                Console.ResetColor();
                string firstName = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Last Name: ");
                Console.ResetColor();
                string lastName = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Date of Birth (yyyy-MM-dd): ");
                Console.ResetColor();
                DateTime dateOfBirth;
                while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
                {
                    Console.WriteLine("Invalid date format. Please enter again (yyyy-MM-dd): ");
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Profile Picture: ");
                Console.ResetColor();
                string profilePicture = Console.ReadLine();


                newUser = new Users
                {
                    UserName = username,
                    Password = password,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    ProfilePicture = profilePicture
                };
                return newUser;
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    Invalid input format : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }

        }

        public bool CheckUsername (string username)
        {
            try
            {
                using(SqlConnection connection = DBConnUtil.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Users WHERE Username = @Username";
                    cmd.Parameters.AddWithValue("@Username", username);
                    connection.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return false;
                        }
                    }
                }
                return true;
            }catch(Exception ex) { 
                Console.WriteLine (ex.Message);
                return false;
            }
        }
        
        public Users GetUserProfile(string username)
        {     
            try
            {
                Users userProfile = null;
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT * FROM Users WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userProfile = new Users
                                {
                                    UserId = Convert.ToInt32(reader["UserID"]),
                                    UserName = reader["Username"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                };
                            }
                        }
                    }
                }
                return userProfile;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred while getting user profile : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return null;
            }
        }

        public bool Logout()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logging out...");
                Console.ResetColor();
                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"    An error occurred during logout : {ex.Message}  ");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                return false;
            }
        }
    }
}
