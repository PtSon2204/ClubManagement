using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClubManagementUI.Validation
{
    public class Validate
    {
        public static bool GetFormatAddUser_SON(string fullname, string password, string role, string email, object? clubId)
        {
            if (string.IsNullOrWhiteSpace(fullname) || string.IsNullOrWhiteSpace(role) ||  string.IsNullOrWhiteSpace(password) 
                || string.IsNullOrWhiteSpace(email) || clubId == null)
            {
                MessageBox.Show("Please enter full!!!", "Not empty!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            return true;
        }
    }
}
