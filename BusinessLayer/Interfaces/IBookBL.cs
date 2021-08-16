using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IBookBL
    {
        AdminBookResponseData AddBook(int adminId, AddBooks adminbookData);
        public List<AdminBookResponseData> GetListOfBooks();

        public bool DeleteBookById(int adminId, string id);
    }
}
