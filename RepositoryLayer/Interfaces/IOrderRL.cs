using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderRL
    {
        public bool AddOrder(int UserId, int CartId, int AddressId);
        public List<OrderResponse> GetListOfOrders(int UserId);
        public List<OrderResponse> GetOrders(int UserId, int CartId);
        public bool DeleteOrderById(int UserId, int OrderId);
    }
}
