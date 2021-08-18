using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAddressBL
    {
        public AddressResponseData AddAddress(int UserId, AddressRequest address);
        public List<AddressResponseData> GetListOfAddress(int UserId);
        public List<AddressResponseData> GetListOfAddressid(int UserId, int addressId);
        public bool UpdateAddress(int UserId, int AddressId, AddressRequest address);
        public bool DeleteAddressById(int UserId, int addressid);
    }
}
