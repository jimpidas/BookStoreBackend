using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CartRL: ICartRL
    {
        private readonly IConfiguration _configuration;
        private SqlConnection connection;

        public int OrderQuantity { get; private set; }

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
                SqlCommand cmdd = new SqlCommand("select * from Carts where BookId='" + BookId + "'AND UserId ='" + UserId + "' ", connection);

                connection.Open();
                SqlDataReader rd = cmdd.ExecuteReader();



                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        OrderQuantity = Convert.ToInt32(rd["OrderQuantity"]);
                    }
                    connection.Close();

                    using (SqlCommand cmd = new SqlCommand("sp_AddBookIntoCartQauntity", connection))
                    {
                       
                        int addQuantity = OrderQuantity + 1;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@BookId", BookId);
                        cmd.Parameters.AddWithValue("@OrderQuantity", addQuantity);
                       
                        connection.Open();
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        connection.Close();

                    };
                    GetListOfBooksInCart(UserId);
                }
                else
                {
                    connection.Close();
                    using (SqlCommand cmd = new SqlCommand("sp_AddBookIntoCart", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@BookId", BookId);
                        connection.Open();
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        connection.Close();
                    };
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IncreaseBookQuantityintoCart(int UserId, int BookId)
        {
            try
            {
                SQLConnection();
                SqlCommand cmdd = new SqlCommand("select * from Carts where BookId='" + BookId + "'AND UserId ='" + UserId + "' ", connection);

                connection.Open();
                SqlDataReader rd = cmdd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        OrderQuantity = Convert.ToInt32(rd["OrderQuantity"]);
                    }
                    connection.Close();

                    using (SqlCommand cmd = new SqlCommand("sp_AddBookIntoCartQauntity", connection))
                    {
                       
                        int addQuantity = OrderQuantity + 1;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@BookId", BookId);
                        cmd.Parameters.AddWithValue("@OrderQuantity", addQuantity);
                        connection.Open();
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        connection.Close();

                    };
                    GetListOfBooksInCart(UserId);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DecreaseBookQuantityintoCart(int UserId, int BookId)
        {
            try
            {
                SQLConnection();
                SqlCommand cmdd = new SqlCommand("select * from Carts where BookId='" + BookId + "'AND UserId ='" + UserId + "' ", connection);

                connection.Open();
                SqlDataReader rd = cmdd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        OrderQuantity = Convert.ToInt32(rd["OrderQuantity"]);
                    }
                    connection.Close();

                    using (SqlCommand cmd = new SqlCommand("sp_removeBookIntoCartQauntity", connection))
                    {
                        int addQuantity = OrderQuantity - 1;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@BookId", BookId);
                        cmd.Parameters.AddWithValue("@OrderQuantity", addQuantity);
                        connection.Open();
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        connection.Close();

                    };
                    GetListOfBooksInCart(UserId);
                }
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
                    connection.Close();
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
                        Price = Convert.ToInt32(dataReader["Price"]),
                        OrderQuantity = Convert.ToInt32(dataReader["OrderQuantity"]),
                        TotalPrice = Convert.ToInt32(dataReader["TotalPrice"]),
                    };
                    bookList.Add(responseData);
                    SQLConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateTotalPrice", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BookId", responseData.BookId);
                        cmd.Parameters.AddWithValue("@TotalPrice", responseData.TotalPrice);
                        connection.Open();
                        cmd.ExecuteReader();
                        connection.Close();
                    }
                }
               
                return bookList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool AddBookQuantityintoCart(int UserId, int BookId, int quantity)
        {
            try
            {
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("sp_SelectQuantity", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);

                    SqlParameter BQuantity = new SqlParameter("@Quantity", System.Data.SqlDbType.Int);
                    BQuantity.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(BQuantity);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    string bQuantity = (cmd.Parameters["@Quantity"].Value).ToString();
                    int result = Int32.Parse(bQuantity);
                    if (quantity > result)
                    {
                        Console.WriteLine("Failed");
                        return false;
                    }
                    else
                    {
                        SQLConnection();
                        using (SqlCommand ccmd = new SqlCommand("sp_AddBookQuantityintoCart", connection))
                        {
                            ccmd.CommandType = System.Data.CommandType.StoredProcedure;
                            ccmd.Parameters.AddWithValue("@UserId", UserId);
                            ccmd.Parameters.AddWithValue("@BookId", BookId);
                            ccmd.Parameters.AddWithValue("@OrderQuantity", quantity);
                            connection.Open();
                            SqlDataReader dataReader = ccmd.ExecuteReader();
                        };
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteCartById(int UserId, int id)
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
