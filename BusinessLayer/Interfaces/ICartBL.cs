using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICartBL
    {
        public bool AddBookToCart(int UserId, int BookId);

        public List<CartBookResponse> GetListOfBooksInCart(int UserId);

        public bool DeleteCartById(int UserId, string id);
    }
}
