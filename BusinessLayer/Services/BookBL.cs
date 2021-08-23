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
    public class BookBL: IBookBL
    {
        private IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

       
        public AdminBookResponseData AddBook(int adminId, AddBooks adminbookData)
        {
            AdminBookResponseData adminbookResponseData = bookRL.AddBook(adminId, adminbookData);
            return adminbookResponseData;
        }

        public List<AdminBookResponseData> GetListOfBooks()
        {
            try
            {
                return this.bookRL.GetListOfBooks();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<AdminBookResponseData> GetListOfBooksid(int bookId)
        {
            try
            {
                return this.bookRL.GetListOfBooksid(bookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteBookById(int adminId, int bookId)
        {
            return this.bookRL.DeleteBookById(adminId, bookId);
        }
        public AdminBookResponseData UpdateBook(int bookId, int adminId, AddBooks adminbookData)
        {
            try
            {
                AdminBookResponseData adminbookResponseData = bookRL.UpdateBook(bookId, adminId, adminbookData);
                return adminbookResponseData;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
