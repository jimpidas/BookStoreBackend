using BusinessLayer.Interfaces;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL: IOrderBL
    {
        IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;

        }

        public bool AddOrder(int UserId, int CartId, int AddressId)
        {
            try
            {
                return this.orderRL.AddOrder(UserId, CartId, AddressId);
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
                return this.orderRL.GetListOfOrders(UserId);
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
                return this.orderRL.GetOrders(UserId, CartId);
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
                return this.orderRL.DeleteOrderById(UserId, OrderId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
