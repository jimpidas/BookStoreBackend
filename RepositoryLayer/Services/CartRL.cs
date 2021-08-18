using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CartRL: ICartRL
    {
        private readonly IConfiguration _configuration;
        private SqlConnection connection;
        public CartRL(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public void SQLConnection()
        {
            string sqlConnectionString = _configuration.GetConnectionString("BookStoreDB");
            connection = new SqlConnection(sqlConnectionString);
        }
        public bool AddBookToCart(int UserId, int BookId)
        {
            try
            {

                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("sp_AddBookIntoCart", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                   

                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                   
                };

                return false;
            }
             
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CartBookResponse> GetListOfBooksInCart(int UserId)
        {
            try
            {
                List<CartBookResponse> bookList = null;
                SQLConnection();
                bookList = new List<CartBookResponse>();
                using (SqlCommand cmd = new SqlCommand("sp_GetListOfBooksInCart", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    bookList = ListBookCartResponseModel(dataReader);
                };
                return bookList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<CartBookResponse> ListBookCartResponseModel(SqlDataReader dataReader)
        {
            try
            {
                List<CartBookResponse> bookList = new List<CartBookResponse>();
                CartBookResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new CartBookResponse
                    {
                        CartId = Convert.ToInt32(dataReader["CartId"]),
                        BookId = Convert.ToInt32(dataReader["BookId"]),
                        Name = dataReader["Name"].ToString(),
                        Author = dataReader["Author"].ToString(),
                        Language = dataReader["Language"].ToString(),
                        Category = dataReader["Category"].ToString(),
                        Pages = dataReader["Pages"].ToString(),
                        Price = dataReader["Price"].ToString()
                    };
                    bookList.Add(responseData);
                }
                return bookList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteCartById(int UserId, string id)
        {
            try
            {
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("sp_DeleteCartByIdProcedure", connection))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", id);

                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
