using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration _configuration;
        private SqlConnection connection;
        public UserRL(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void SQLConnection()
        {
            string sqlConnectionString = _configuration.GetConnectionString("BookStoreDB");
            connection = new SqlConnection(sqlConnectionString);
        }

        public void RegisterUser(RegisterUserRequest user)
        {
            try
            {
                UserResponse responseData = null;

                SQLConnection();
                string encryptedPassword = StringCipher.Encrypt(user.Password);

                using (SqlCommand cmd = new SqlCommand("SpUserRegisterProcedure", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", user.FullName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                    cmd.Parameters.AddWithValue("@MobileNo", user.MobileNo);
                    cmd.Parameters.AddWithValue("@Role", "Customer");

                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    responseData = RegistrationResponseModel(dataReader);
                };
                // return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private UserResponse RegistrationResponseModel(SqlDataReader dataReader)
        {
            try
            {
                UserResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new UserResponse
                    {
                        //  UserId = Convert.ToInt32(dataReader["UserID"]),
                        FullName = dataReader["FullName"].ToString(),
                        MobileNo=dataReader["MobileNo"].ToString(),
                        Email = dataReader["Email"].ToString(),
                        Password = dataReader["Password"].ToString()

                    };
                }
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Login(string email, string password)
        {
            try
            {
                // UserResponce responseData = null;

                SQLConnection();
                string encryptedPassword = StringCipher.Encrypt(password);

                SqlCommand cmd = new SqlCommand("SpUserLogin", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", encryptedPassword);

                SqlParameter userId = new SqlParameter("@UserId", System.Data.SqlDbType.Int);
                userId.Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(userId);
                connection.Open();
                cmd.ExecuteNonQuery();
                string ID = (cmd.Parameters["@UserId"].Value).ToString();

                connection.Close();
                connection.Dispose();


                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("Hello This Token Is Genereted");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email",email),
                    new Claim("UserId",ID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(7),
                    SigningCredentials =
               new SigningCredentials(
                   new SymmetricSecurityKey(tokenKey),
                   SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }


            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}


