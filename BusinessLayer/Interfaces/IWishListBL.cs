using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IWishListBL
    {
        public WishListRequest AddBookToWishList(int UserId, int BookId);
        public List<WishListBookResponse> GetListOfBooksInWishlist(int UserId);
    }
}
