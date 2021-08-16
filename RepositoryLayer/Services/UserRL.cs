using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
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
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SpUserRegisterProcedure", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", user.FullName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                    cmd.Parameters.AddWithValue("@MobileNo", user.MobileNo);
                    cmd.Parameters.AddWithValue("@Role", "Customer");
                    var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    //connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    var result = returnParameter.Value;
                    if (result != null && result.Equals(2))
                    {
                        throw new Exception("Email already registered");
                    }

                    responseData = RegistrationResponseModel(dataReader);
                };
               
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


        public bool ForgotPassword(string email)
        {
            try
            {
                SQLConnection();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Users WHERE Email='" + email + "'", connection);
                DataTable dt = new DataTable();

                sda.Fill(dt);
                if (dt.Rows.Count < 1)
                {
                    return false;
                }

                MessageQueue queue;

               
                if (MessageQueue.Exists(@".\private$\BookStore"))
                {
                    queue = new MessageQueue(@".\private$\BookStore");
                }
                else
                {
                    queue = MessageQueue.Create(@".\Private$\BookStore");
                }

                Message MyMessage = new Message();
                MyMessage.Formatter = new BinaryMessageFormatter();
                MyMessage.Body = email;
                MyMessage.Label = "Forget Password Email BookStore Application";
                queue.Send(MyMessage);
                Message msg = queue.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendEmail(msg.Body.ToString(), GenerateToken(msg.Body.ToString()));
                queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                queue.BeginReceive();
                queue.Close();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {

            MessageQueue queue = (MessageQueue)sender;
            Message msg = queue.EndReceive(e.AsyncResult);
            EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
            queue.BeginReceive();
        }

        public string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email",email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public void ChangePassword(string email, string newPassword)
        {
            try
            {
                SQLConnection();
                string encryptedPassword = StringCipher.Encrypt(newPassword);
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Users] SET[Password] ='" + encryptedPassword + "' WHERE Email ='" + email + "' ", connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}


