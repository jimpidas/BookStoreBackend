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

        // Add Notes
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


        public bool DeleteBookById(int adminId, string id)
        {
            return this.bookRL.DeleteBookById(adminId, id);
        }
    }
}
