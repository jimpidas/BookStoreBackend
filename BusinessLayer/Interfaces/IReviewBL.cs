using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IReviewBL
    {
        public ReviewRequest AddReview(int bookId, int UserId, ReviewRequest review);
        public List<ReviewListBookResponse> GetListOfReview(int UserId);
    }
}
