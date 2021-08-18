using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRL
    {
        public bool AddBookToCart(int UserId, int BookId);


        public List<CartBookResponse> GetListOfBooksInCart(int UserId);

        public bool DeleteCartById(int UserId, string id);
    }
}
