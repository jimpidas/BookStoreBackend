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
        public bool AddBookQuantityintoCart(int UserId, int BookId, int quantity);
        public bool IncreaseBookQuantityintoCart(int UserId, int BookId);
        public bool DecreaseBookQuantityintoCart(int UserId, int BookId);
        public bool DeleteCartById(int UserId, int id);
    }
}
