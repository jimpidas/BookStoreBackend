using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrderRL: IOrderRL
    {
        private readonly IConfiguration _configuration;
        private SqlConnection connection;



        public OrderRL(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public void SQLConnection()
        {
            string sqlConnectionString = _configuration.GetConnectionString("BookStoreDB");
            connection = new SqlConnection(sqlConnectionString);
        }

        public bool AddOrder(int UserId, int CartId, int AddressId)
        {
            try
            {

                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("sp_AddOrder", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@CartId", CartId);
                    cmd.Parameters.AddWithValue("@AddressId", AddressId);

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
        public List<OrderResponse> GetListOfOrders(int UserId)
        {

            try
            {
                List<OrderResponse> bookList = null;
                SQLConnection();
                bookList = new List<OrderResponse>();
                using (SqlCommand cmd = new SqlCommand("sp_GetListOrder", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

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
        public List<OrderResponse> GetOrders(int UserId, int CartId)
        {
           
            try
            {
                List<OrderResponse> bookList = null;
                SQLConnection();
                bookList = new List<OrderResponse>();
                using (SqlCommand cmd = new SqlCommand("sp_GetOrder", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@CartId", CartId);

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
        private List<OrderResponse> ListBookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                List<OrderResponse> bookList = new List<OrderResponse>();
                OrderResponse responseData = null;
                while (dataReader.Read())
                {
                    responseData = new OrderResponse
                    {
                        UserId = Convert.ToInt32(dataReader["UserId"]),
                        CartId = Convert.ToInt32(dataReader["CartId"]),
                        AddressId = Convert.ToInt32(dataReader["AddressId"]),
                        OrderId = Convert.ToInt32(dataReader["OrderId"]),
                        BookId = Convert.ToInt32(dataReader["BookId"]),
                        Name = dataReader["Name"].ToString(),
                        Author = dataReader["Author"].ToString(),
                        Language = dataReader["Language"].ToString(),
                        CustomerName = dataReader["CustomerName"].ToString(),
                        City = dataReader["City"].ToString(),
                        State = dataReader["State"].ToString(),
                        Country = dataReader["Country"].ToString(),
                        Pincode = dataReader["Pincode"].ToString(),
                        MobileNumber = dataReader["MobileNumber"].ToString(),
                       
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
        public bool DeleteOrderById(int UserId, int OrderId)
        {
            try
            {
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("sp_DeleteOrderById", connection))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderId", OrderId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);

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
