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
    public class AddressRL: IAddressRL
    {
        private readonly IConfiguration _configuration;
        private SqlConnection connection;
        public AddressRL(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public void SQLConnection()
        {
            string sqlConnectionString = _configuration.GetConnectionString("BookStoreDB");
            connection = new SqlConnection(sqlConnectionString);
        }


        public AddressResponseData AddAddress(int UserId, AddressRequest address)
        {
            try
            {
                AddressResponseData addressResponseData = null;
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("sp_AddAddressProcedure", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@CustomerName", address.CustomerName);
                    cmd.Parameters.AddWithValue("@City", address.City);
                    cmd.Parameters.AddWithValue("@State", address.State);
                    cmd.Parameters.AddWithValue("@Country", address.Country);
                    cmd.Parameters.AddWithValue("@Pincode", address.Pincode);
                    cmd.Parameters.AddWithValue("@MobileNumber", address.MobileNumber);


                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                  addressResponseData = BookResponseModel(dataReader);
                }
                return addressResponseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private AddressResponseData BookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                AddressResponseData responseData = null;
                while (dataReader.Read())
                {
                    responseData = new AddressResponseData
                    {
                        UserId = Convert.ToInt32(dataReader["UserId"]),
                        CustomerName = dataReader["CustomerName"].ToString(),
                        City = dataReader["City"].ToString(),
                        State = dataReader["State"].ToString(),
                        Country = dataReader["Country"].ToString(),
                        Pincode = dataReader["Pincode"].ToString(),
                        MobileNumber = dataReader["MobileNumber"].ToString()
                    };
                }
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AddressResponseData> GetListOfAddress(int UserId)
        {
            try
            {
                List<AddressResponseData> bookList = null;
                SQLConnection();
                bookList = new List<AddressResponseData>();
                using (SqlCommand cmd = new SqlCommand("sp_GetListOfAddress", connection))
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


        public List<AddressResponseData> GetListOfAddressid(int UserId, int addressId)
        {
            try
            {
                List<AddressResponseData> bookList = null;
                SQLConnection();
                bookList = new List<AddressResponseData>();
                using (SqlCommand cmd = new SqlCommand("sp_GetListOfAddressid", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AddressId", addressId);

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
        private List<AddressResponseData> ListBookResponseModel(SqlDataReader dataReader)
        {
            try
            {
                List<AddressResponseData> bookList = new List<AddressResponseData>();
                AddressResponseData responseData = null;
                while (dataReader.Read())
                {
                    responseData = new AddressResponseData
                    {
                        UserId = Convert.ToInt32(dataReader["UserId"]),
                        AddressId = Convert.ToInt32(dataReader["AddressId"]),
                        CustomerName = dataReader["CustomerName"].ToString(),
                        City = dataReader["City"].ToString(),
                        State = dataReader["State"].ToString(),
                        Country = dataReader["Country"].ToString(),
                        Pincode = dataReader["Pincode"].ToString(),
                        MobileNumber = dataReader["MobileNumber"].ToString()
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

        public bool UpdateAddress(int UserId, int AddressId, AddressRequest address)
        {
            try
            {
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("sp_UpdateAddress", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AddressId", AddressId);
                    cmd.Parameters.AddWithValue("@CustomerName", address.CustomerName);
                    cmd.Parameters.AddWithValue("@City", address.City);
                    cmd.Parameters.AddWithValue("@State", address.State);
                    cmd.Parameters.AddWithValue("@Country", address.Country);
                    cmd.Parameters.AddWithValue("@Pincode", address.Pincode);
                    cmd.Parameters.AddWithValue("@MobileNumber", address.MobileNumber);


                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                };
                return true;


            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteAddressById(int UserId, int addressid)
        {
            try
            {
                SQLConnection();
                using (SqlCommand cmd = new SqlCommand("sp_DeleteAddressById", connection))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AddressId", addressid);

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
