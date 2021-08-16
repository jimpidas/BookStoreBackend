using CommonLayer.RequestModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
       
        void RegisterUser(RegisterUserRequest user);
        string Login(string email, string password);
        bool ForgotPassword(string email);
        void ChangePassword(string email, string newPassword);

    }
}
