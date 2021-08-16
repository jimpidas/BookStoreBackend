using BusinessLayer.Interfaces;
using CommonLayer.RequestModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL: IAdminBL
    {
        private IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }


        public void RegisterAdmin(AdminRegisterRequest admin)
        {
            try
            {
                this.adminRL.RegisterAdmin(admin);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public string Login(string email, string password)
        {
            try
            {
                return this.adminRL.Login(email, password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Forgot Password
        public bool ForgotPassword(string email)
        {
            try
            {
                return this.adminRL.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        // Change Password
        public void ChangePassword(string email, string newPassword)
        {
            try
            {
                this.adminRL.ChangePassword(email, newPassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
