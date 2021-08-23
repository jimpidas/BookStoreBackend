using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
        AdminBookResponseData AddBook(int adminId, AddBooks adminbookData);
        public List<AdminBookResponseData> GetListOfBooks();
        public List<AdminBookResponseData> GetListOfBooksid(int bookId);
        public bool DeleteBookById(int adminId, int bookId);
        public AdminBookResponseData UpdateBook(int bookId, int adminId, AddBooks adminbookData);
    }
}
