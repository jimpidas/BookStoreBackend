using BusinessLayer.Interfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL: IWishListBL
    {
        IWishListRL wishListRL;
        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }
        public WishListRequest AddBookToWishList(int UserId, int BookId)
        {
            try
            {
                return this.wishListRL.AddBookToWishList(UserId, BookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<WishListBookResponse> GetListOfBooksInWishlist(int UserId)
        {
            try
            {
                return this.wishListRL.GetListOfBooksInWishlist(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
