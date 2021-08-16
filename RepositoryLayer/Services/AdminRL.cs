using CommonLayer.RequestModel;
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
    public class AdminRL: IAdminRL
    {
        private readonly IConfiguration _configuration;
        private SqlConnection connection;

        public AdminRL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SQLConnection()
        {
            string sqlConnectionString = _configuration.GetConnectionString("BookStoreDB");
            connection = new SqlConnection(sqlConnectionString);
        }

        public void RegisterAdmin(AdminRegisterRequest admin)
        {
            try
            {

                SQLConnection();
                string encryptedPassword = StringCipher.Encrypt(admin.Password);
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_AdminRegisterProcedure", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", admin.FullName);
                    cmd.Parameters.AddWithValue("@Email", admin.Email);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                    cmd.Parameters.AddWithValue("@MobileNo", admin.MobileNo);
                    cmd.Parameters.AddWithValue("@Role", "Admin");
                    var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    //connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    var result = returnParameter.Value;
                    if (result != null && result.Equals(2))
                    {
                        throw new Exception("Email already registered");
                    }
                };
               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public string Login(string email, string password)
        {
            try
            {
                SQLConnection();
                string encryptedPassword = StringCipher.Encrypt(password);

                SqlCommand cmd = new SqlCommand("sp_AdminLogin", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", encryptedPassword);

                SqlParameter adminId = new SqlParameter("@AdminId", System.Data.SqlDbType.Int);
                adminId.Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(adminId);
                connection.Open();
                cmd.ExecuteNonQuery();
                string ID = (cmd.Parameters["@AdminId"].Value).ToString();

                connection.Close();
                connection.Dispose();



                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("Hello This Token Is Genereted");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
               {
                    new Claim("Email",email),
                    new Claim("AdminId",ID.ToString())
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
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Admin WHERE Email='" + email + "'", connection);
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
                    queue = MessageQueue.Create(@".\private$\BookStore");
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
                queue.BeginReceive(TimeSpan.FromSeconds(5));
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
            queue.BeginReceive(TimeSpan.FromSeconds(5));


        }
        // Generate Token
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
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Admin] SET[Password] ='" + encryptedPassword + "' WHERE Email ='" + email + "' ", connection);
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
