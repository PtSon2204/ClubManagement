using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ClubManagement.DAL.Entities;
using System.Net;
using System.Net.Mail;
using ClubManagement.DAL;

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
        public static bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                string fromEmail = "sonpthe181997@fpt.edu.vn"; // Đổi thành email bạn
                string appPassword = "unvc alcg cqtl nesr";   // Đổi thành app password 16 ký tự

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromEmail);
                message.To.Add(new MailAddress(toEmail));
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = false;

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential(fromEmail, appPassword);
                client.EnableSsl = true;
                client.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gửi email thất bại: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        //hàm này tạo mk ngẫu nhiên cho chức năng quên mk
        public static string GenerateRandomPassword(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool IsChairmanOrViceExists(int clubId, string role, int? excludeUserId = null)
        {
            using var context = new ClubManagementContext();
            return context.Users
                .Any(u => u.ClubId == clubId &&
                          (u.Role == "Chairman" || u.Role == "ViceChairman") &&
                          (excludeUserId == null || u.UserId != excludeUserId));
        }
    }
}
