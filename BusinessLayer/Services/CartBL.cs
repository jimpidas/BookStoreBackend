using BusinessLayer.Interfaces;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL: ICartBL
    {
        private ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public bool AddBookToCart(int UserId, int BookId)
        {
            try
            {
                return this.cartRL.AddBookToCart(UserId, BookId);
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
                return this.cartRL.GetListOfBooksInCart(UserId);
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
                return this.cartRL.AddBookQuantityintoCart(UserId, BookId, quantity);
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
                return this.cartRL.IncreaseBookQuantityintoCart(UserId, BookId);
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
                return this.cartRL.DecreaseBookQuantityintoCart(UserId, BookId);
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
                return this.cartRL.DeleteCartById(UserId, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
