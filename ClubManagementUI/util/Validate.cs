using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ClubManagement.DAL.Entities;

namespace ClubManagementUI.Validation
{
    public class Validate
    {
        public static bool NotDuplicationEmail(List<User> user, string email)
        {
            //if (users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase))) => cách dùng LINQ để cho ngắn gọn hơn

            foreach (var item in user)
            {
                if (item.Email.ToLower().Equals(email.ToLower()))
                {
                    MessageBox.Show("Email exists!", "Enter again!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return false;
                }
            }
            return true;
        }
        public static bool GetFormatAddUser_SON(string fullname, string password, string role, string email, object? clubId)
        {
            if (string.IsNullOrWhiteSpace(fullname) || string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(password)
                || string.IsNullOrWhiteSpace(email) || clubId == null)
            {
                MessageBox.Show("Please enter full!!!", "Not empty!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            return true;
        }
    }
}
