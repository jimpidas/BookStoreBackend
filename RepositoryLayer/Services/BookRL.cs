using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL: IBookRL
    {
        private readonly IConfiguration _configuration;
        private SqlConnection connection;
        public BookRL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SQLConnection()
        {
            string sqlConnectionString = _configuration.GetConnectionString("BookStoreDB");
            connection = new SqlConnection(sqlConnectionString);
        }

        public AdminBookResponseData AddBook(int adminId, AddBooks adminbookData)
        {
            try
            {
                AdminBookResponseData adminbookResponseData = null;
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("sp_AddBooksProcedure", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdminId", adminId);
                    cmd.Parameters.AddWithValue("@Name", adminbookData.Name);
                    cmd.Parameters.AddWithValue("@Author", adminbookData.Author);
                    cmd.Parameters.AddWithValue("@Language", adminbookData.Language);
                    cmd.Parameters.AddWithValue("@Category", adminbookData.Category);
                    cmd.Parameters.AddWithValue("@Pages", adminbookData.Pages);
                    cmd.Parameters.AddWithValue("@Price", adminbookData.Price);
                    cmd.Parameters.AddWithValue("@Quantity", adminbookData.Quantity);
                    //cmd.Parameters.AddWithValue("@InStock", adminbookData.InStock);
                    
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    adminbookResponseData = BookResponseModel(dataReader);
                }
                return adminbookResponseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private AdminBookResponseData BookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                AdminBookResponseData responseData = null;
                while (dataReader.Read())
                {
                    responseData = new AdminBookResponseData
                    {
                        BookId = Convert.ToInt32(dataReader["BookID"]),
                        Name = dataReader["Name"].ToString(),
                        Author = dataReader["Author"].ToString(),
                        Language = dataReader["Language"].ToString(),
                        Category = dataReader["Category"].ToString(),
                        Pages = dataReader["Pages"].ToString(),
                        Price = Convert.ToInt32(dataReader["Price"]),
                        Quantity = Convert.ToInt32(dataReader["Quantity"])
                       
                    };
                }
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AdminBookResponseData> GetListOfBooks()
        {
            try
            {
                List<AdminBookResponseData> bookList = null;
                SQLConnection();
                bookList = new List<AdminBookResponseData>();
                using (SqlCommand cmd = new SqlCommand("sp_GetListOfBooks", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    bookList = ListBookResponseModel(dataReader);
                };
                return bookList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AdminBookResponseData> GetListOfBooksid(int bookId)
        {
            try
            {
                List<AdminBookResponseData> bookList = null;
                SQLConnection();
                bookList = new List<AdminBookResponseData>();
                using (SqlCommand cmd = new SqlCommand("sp_GetListOfBooksId", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    bookList = ListBookResponseModel(dataReader);
                };
                return bookList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private List<AdminBookResponseData> ListBookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                List<AdminBookResponseData> bookList = new List<AdminBookResponseData>();
                AdminBookResponseData responseData = null;
                while (dataReader.Read())
                {
                    responseData = new AdminBookResponseData
                    {
                        BookId = Convert.ToInt32(dataReader["BookId"]),
                        Name = dataReader["Name"].ToString(),
                        Author = dataReader["Author"].ToString(),
                        Language = dataReader["Language"].ToString(),
                        Category = dataReader["Category"].ToString(),
                        Pages = dataReader["Pages"].ToString(),
                        Price = Convert.ToInt32(dataReader["Price"]),
                        Quantity = Convert.ToInt32(dataReader["Quantity"])
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
        public AdminBookResponseData UpdateBook(int bookId, int adminId, AddBooks adminbookData)
        {
            try
            {
                AdminBookResponseData adminbookResponseData = null;
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("sp_UpdateBooksProcedure", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@AdminId", adminId);
                    cmd.Parameters.AddWithValue("@Name", adminbookData.Name);
                    cmd.Parameters.AddWithValue("@Author", adminbookData.Author);
                    cmd.Parameters.AddWithValue("@Language", adminbookData.Language);
                    cmd.Parameters.AddWithValue("@Category", adminbookData.Category);
                    cmd.Parameters.AddWithValue("@Pages", adminbookData.Pages);
                    cmd.Parameters.AddWithValue("@Price", adminbookData.Price);
                    cmd.Parameters.AddWithValue("@Quantity", adminbookData.Quantity);

                    //cmd.Parameters.AddWithValue("@InStock", adminbookData.InStock);
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    adminbookResponseData = BookResponseModel(dataReader);
                }
                return adminbookResponseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteBookById(int adminId, int bookId)
        {
            try
            {
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("sp_DeleteBookByIdProcedure", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@AdminId", adminId);
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
