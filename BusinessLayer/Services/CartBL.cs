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
        ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public bool AddBookToCart(int UserId, int BookId)
        {
            return this.cartRL.AddBookToCart(UserId, BookId);
        }

        public List<CartBookResponse> GetListOfBooksInCart(int UserId)
        {
            try
            {
                return this.cartRL.GetListOfBooksInCart(UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool DeleteCartById(int UserId, string id)
        {
            return this.cartRL.DeleteCartById(UserId, id);
        }
    }
}
